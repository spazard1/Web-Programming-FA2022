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

        Task<ImageModel> GetAsync(string id);

        Task<ImageModel> AddOrUpdateAsync(ImageModel image);

        Task DeleteAsync(string id);

        string GetBlobUrl();

        string GetUploadUrl(string fileName);

        string GetDownloadUrl(ImageModel image);

        IAsyncEnumerable<ImageModel> GetAllImagesAsync();

        Task PurgeAsync();
    }
}
