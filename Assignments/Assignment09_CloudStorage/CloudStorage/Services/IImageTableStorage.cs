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

        Task<bool> DeleteAsync(string id);

        string GetUploadUrl(ImageTableEntity image);

        string GetDownloadUrl(ImageTableEntity image);

        IAsyncEnumerable<ImageTableEntity> GetAllImagesAsync();

        Task PurgeAsync();
    }
}
