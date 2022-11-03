using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using CloudStorage.Services;
using CloudStorage.Entities;

namespace CloudStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageTableStorage imageTableStorage;

        public ImagesController(IImageTableStorage imageTableStorage)
        {
            this.imageTableStorage = imageTableStorage;
        }

        [HttpGet]
        public IAsyncEnumerable<ImageEntity> GetAsync()
        {
            return imageTableStorage.GetAllImagesAsync().Select(image => new ImageEntity(image));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var imageModel = await this.imageTableStorage.GetAsync(id);

            // TODO: check to make sure imageModel is not null
            // if it is null (i.e. it doesn't exist), return not found

            // TODO: set Cache-Control header here, it is in seconds; should be cached for seven hours
            // Make sure the format of the header is correct. There is more to the header's value than just an int.

            // TODO: return actual download url in the Location header
            Response.Headers["Location"] = string.Empty;

            // TODO: OK is not the correct status code here. Update it.
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ImageEntity imageEntity)
        {
            // TODO: Convert the image entity into an image model, then add it into the database.
            // Remember to set the username property on the image model before it is added.

            // TODO: Return a new image entity to the client. Set the uploadUrl first so they can start the image upload.
            // Be careful here to return a new image entity based on the image model that you created, and not the image entity that was sent to your controller.
            return null;
        }

        [HttpPut("{id}/uploadComplete")]
        public async Task<IActionResult> UploadCompleteAsync(string id)
        {
            // TODO: Get the image model from the database by its id.

            // TODO: check to make sure image model is not null
            // if it is null (i.e. it doesn't exist), return a NotFound status code

            // TODO: Set UploadComplete to true on the imageModel and then save it.

            // TODO: Convert the image model into an ImageEntity and return it as JSON.
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this.imageTableStorage.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<IActionResult> PurgeAsync()
        {
            await this.imageTableStorage.PurgeAsync();
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}