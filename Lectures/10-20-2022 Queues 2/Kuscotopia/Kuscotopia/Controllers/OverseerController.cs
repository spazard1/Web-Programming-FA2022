using Common.Entities;
using Kuscotopia.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kuscotopia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverseerController : ControllerBase
    {
        private readonly QueueService queueService;

        public OverseerController(QueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpPost]
        public async Task<IActionResult> QueueWorkAsync([FromBody] WorkerEntity workerEntity)
        {
            await queueService.QueueWorkAsync(workerEntity);

            return StatusCode((int)HttpStatusCode.Accepted);
        }
    }
}