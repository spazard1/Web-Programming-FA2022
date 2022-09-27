using Hobbits.Entities;
using Hobbits.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hobbits.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HobbitsController : Controller
    {

        private readonly static HobbitsDatabase hobbitsDatabase = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Json(hobbitsDatabase.GetAll().Select(hobbit => new HobbitEntity(hobbit)));
        }

        [HttpGet("{index:int}")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= hobbitsDatabase.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Json(new HobbitEntity(hobbitsDatabase.Get(index)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] HobbitEntity hobbitEntity)
        {
            hobbitsDatabase.Add(hobbitEntity.ToModel());

            return Json(hobbitEntity);
        }
    }
}