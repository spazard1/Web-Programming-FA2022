using Azure.Storage.Blobs;

namespace CloudStorage.Services
{
    public class BlobServiceClientProvider : IBlobServiceClientProvider
    {
        private readonly IConnectionStringProvider connectionStringProvider;

        public BlobServiceClientProvider(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public BlobServiceClient BlobServiceClient => new(connectionStringProvider.ConnectionString);
    }
}
