using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CloudStorage.Models;
using Microsoft.Azure.Cosmos.Table;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage;
using Azure.Storage.Blobs.Models;

namespace CloudStorage.Services
{
    public class ImageTableStorage : IImageTableStorage
    {
        private readonly IUserNameProvider userNameProvider;
        private readonly IConnectionStringProvider connectionStringProvider;
        private CloudStorageAccount cloudStorageAccount;
        private CloudTable imageTable;
        private BlobServiceClient blobServiceClient;
        private BlobContainerClient blobContainerClient;

        public ImageTableStorage(IUserNameProvider userNameProvider, ICloudStorageAccountProvider cloudStorageAccountProvider, IConnectionStringProvider connectionStringProvider)
        {
            this.userNameProvider = userNameProvider;
            this.connectionStringProvider = connectionStringProvider;
            cloudStorageAccount = cloudStorageAccountProvider.CloudStorageAccount;
            var tableClient = cloudStorageAccount.CreateCloudTableClient();
            imageTable = tableClient.GetTableReference(userNameProvider.UserName);

            blobServiceClient = cloudStorageAccountProvider.BlobServiceClient;
            blobContainerClient = blobServiceClient.GetBlobContainerClient(userNameProvider.UserName);
        }

        public async Task StartupAsync()
        {
            await imageTable.CreateIfNotExistsAsync();
            await blobContainerClient.CreateIfNotExistsAsync();
        }
        
        public async Task<ImageTableEntity> GetAsync(string id)
        {
            TableResult retrievedResult = await imageTable.ExecuteAsync(TableOperation.Retrieve<ImageTableEntity>(this.userNameProvider.UserName, id));
            return (ImageTableEntity) retrievedResult.Result;
        }

        public async Task<ImageTableEntity> AddOrUpdateAsync(ImageTableEntity image)
        {
            if (string.IsNullOrWhiteSpace(image.Id))
            {
                image.Id = Guid.NewGuid().ToString();
                image.UserName = this.userNameProvider.UserName;
            }
            await imageTable.ExecuteAsync(TableOperation.InsertOrReplace(image));
            return image;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var imageTableEntity = await GetAsync(id);
            if (imageTableEntity == null)
            {
                return false;
            }
            await imageTable.ExecuteAsync(TableOperation.Delete(imageTableEntity));
            return true;
        }

        public string GetUploadUrl(ImageTableEntity image)
        {
            // Create a SAS token that's valid for one hour.
            BlobSasBuilder sasBuilderBlob = new BlobSasBuilder()
            {
                BlobContainerName = blobContainerClient.Name,
                BlobName = image.Id,
                Resource = "b",
            };
            sasBuilderBlob.StartsOn = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(15));
            sasBuilderBlob.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
            sasBuilderBlob.SetPermissions(BlobSasPermissions.Write | BlobSasPermissions.Add | BlobSasPermissions.Create);

            // Use the key to get the SAS token.
            var sasToken = sasBuilderBlob.ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, connectionStringProvider.AccountKey)).ToString();

            return blobContainerClient.GetBlockBlobClient(image.Id).Uri + "?" + sasToken;
        }

        public string GetDownloadUrl(ImageTableEntity image)
        {
            // Create a SAS token that's valid for one hour.
            BlobSasBuilder sasBuilderBlob = new BlobSasBuilder()
            {
                BlobContainerName = blobContainerClient.Name,
                BlobName = image.Id,
                Resource = "b",
            };
            sasBuilderBlob.StartsOn = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(15));
            sasBuilderBlob.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
            sasBuilderBlob.SetPermissions(BlobSasPermissions.Read);

            // Use the key to get the SAS token.
            var sasToken = sasBuilderBlob.ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, connectionStringProvider.AccountKey)).ToString();

            return blobContainerClient.GetBlockBlobClient(image.Id).Uri + "?" + sasToken;
        }

        public async IAsyncEnumerable<ImageTableEntity> GetAllImagesAsync()
        {
            TableQuery<ImageTableEntity> tableQuery = new TableQuery<ImageTableEntity>();
            TableContinuationToken continuationToken = null;

            do
            {
                TableQuerySegment<ImageTableEntity> tableQueryResult =
                    await imageTable.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                continuationToken = tableQueryResult.ContinuationToken;

                foreach (var imageResult in tableQueryResult.Results)
                {
                    if (imageResult.UploadComplete)
                    {
                        yield return imageResult;
                    }
                }
            } while (continuationToken != null);
        }

        public async Task PurgeAsync()
        {
            TableQuery<ImageTableEntity> tableQuery = new TableQuery<ImageTableEntity>();
            TableContinuationToken tableContinuationToken = null;

            do
            {
                TableQuerySegment<ImageTableEntity> tableQueryResult =
                    await imageTable.ExecuteQuerySegmentedAsync(tableQuery, tableContinuationToken);

                tableContinuationToken = tableQueryResult.ContinuationToken;

                var tasks = tableQueryResult.Results.Select(async result => await imageTable.ExecuteAsync(TableOperation.Delete(result)));
                await Task.WhenAll(tasks);
            } while (tableContinuationToken != null);

            string continuationToken = null;

            do
            {
                var resultSegment = blobContainerClient.GetBlobs().AsPages(continuationToken);

                foreach (Azure.Page<BlobItem> blobPage in resultSegment)
                {
                    foreach (BlobItem blobItem in blobPage.Values)
                    {
                        await blobContainerClient.DeleteBlobIfExistsAsync(blobItem.Name);
                    }

                    continuationToken = blobPage.ContinuationToken;
                }

            } while (continuationToken != "");
        }
    }
}
