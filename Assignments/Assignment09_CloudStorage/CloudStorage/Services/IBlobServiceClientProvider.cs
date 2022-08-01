using Azure.Storage.Blobs;

namespace CloudStorage.Services
{
    public interface IBlobServiceClientProvider
    {
        BlobServiceClient BlobServiceClient { get; }
    }
}
