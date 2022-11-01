using Gargoyles.Entities;
using Gargoyles.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace Gargoyles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GargoylesController : Controller
    {
        private readonly GargoylesDatabase gargoylesDatabase;

        public GargoylesController(GargoylesDatabase gargoylesDatabase)
        {
            this.gargoylesDatabase = gargoylesDatabase;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            // note, i'm not handling the 404 for this lecture, don't forget about it in your assignment.
            var gargoyle = this.gargoylesDatabase.Get(name);

            Response.Headers["ETag"] = gargoyle.ETag();

            return Json(new GargoyleEntity(gargoyle));
        }

        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody] GargoyleEntity gargoyleEntity)
        {
            if (name != gargoyleEntity.Name)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            var existingGargoyle = this.gargoylesDatabase.Get(name);

            if (existingGargoyle != null)
            {
                if (!Request.Headers.TryGetValue("if-match", out StringValues ifMatch))
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Missing the if-match header.");
                }

                if (existingGargoyle.ETag() != ifMatch)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
            }

            this.gargoylesDatabase.AddOrReplace(gargoyleEntity.ToModel());

            return Json(gargoyleEntity);
        }
    }
}