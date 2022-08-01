using CloudStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.Services
{
    public interface IImageTableStorage
    {
        Task StartupAsync();

        Task<ImageTableEntity> GetAsync(string id);

        Task<ImageTableEntity> AddOrUpdateAsync(ImageTableEntity image);

        Task DeleteAsync(string id);

        string GetBlobUrl();

        string GetUploadUrl(string fileName);

        string GetDownloadUrl(ImageTableEntity image);

        IAsyncEnumerable<ImageTableEntity> GetAllImagesAsync();

        Task PurgeAsync();
    }
}
