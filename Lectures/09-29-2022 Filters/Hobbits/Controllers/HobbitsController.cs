using Hobbits.Entities;
using Hobbits.Filters;
using Hobbits.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hobbits.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(RequestIdFilter))]
    [ServiceFilter(typeof(RequestLoggingFilter))]
    public class HobbitsController : Controller
    {

        private readonly HobbitsDatabase hobbitsDatabase;
        private readonly IHobbitLogger hobbitLogger;

        public HobbitsController(HobbitsDatabase hobbitsDatabase, IHobbitLogger hobbitLogger)
        {
            this.hobbitsDatabase = hobbitsDatabase;
            this.hobbitLogger = hobbitLogger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // pretend this is a database call
            var hobbitEntities = hobbitsDatabase.GetAll().Select(hobbit => new HobbitEntity(hobbit));

            this.hobbitLogger.WriteLine("Number of Hobbits: " + hobbitEntities.Count());

            return Json(hobbitEntities);
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