using ApiVersioning.Entities.V1U1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiVersioning.Controllers.V1U1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.1")]
    public class ValuesController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ValuesEntity entity)
        {
            // todo: save the entity to the database.

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
