using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public class CloudStorageAccountProvider : ICloudStorageAccountProvider
    {
        private readonly IConnectionStringProvider connectionStringProvider;

        public CloudStorageAccountProvider(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public CloudStorageAccount CloudStorageAccount => CloudStorageAccount.Parse(connectionStringProvider.ConnectionString);

        public BlobServiceClient BlobServiceClient => new BlobServiceClient(connectionStringProvider.ConnectionString);
    }
}
