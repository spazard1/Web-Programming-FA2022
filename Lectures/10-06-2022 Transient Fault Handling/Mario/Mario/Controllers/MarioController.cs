using Microsoft.AspNetCore.Mvc;
using Mario.Entities;
using Mario.Services;

namespace Mario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarioController : ControllerBase
    {
        private readonly ExternalMarioService externalMarioService;

        public MarioController(ExternalMarioService externalMarioService)
        {
            this.externalMarioService = externalMarioService;
        }

        [HttpGet]
        public async Task<MarioEntity?> GetAsync()
        {
            return await this.externalMarioService.GetMarioLevelStatusAsync();
        }
    }
}