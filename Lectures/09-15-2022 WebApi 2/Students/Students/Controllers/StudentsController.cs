using Microsoft.AspNetCore.Mvc;
using Students.Entities;
using System.Net;

namespace Students.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {

        /*
         * This is ABSOLUTELY NOT the way to do this for real.
         * We will have a MUCH better way once we know dependency injection.
         * The new keyword here is not what we want.
         */
        private static List<StudentEntity> Students { get; set; } = new List<StudentEntity>();

        [HttpGet]
        public List<StudentEntity> Get()
        {
            return Students;
        }

        [HttpGet("{index:int}")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Json(Students[index]);
        }

        [HttpPost]
        public StudentEntity Post([FromBody] StudentEntity studentEntity)
        {
            Students.Add(studentEntity);

            return studentEntity;
        }

        [HttpPut("{index:int}")]
        public IActionResult Put([FromBody] StudentEntity studentEntity, int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            }

            Students[index] = studentEntity;

            return Json(studentEntity);
        }

        [HttpDelete("{index:int}")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            Students.RemoveAt(index);

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPatch("{index:int}")]
        public IActionResult Patch([FromBody] StudentEntity studentEntity, int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(studentEntity.FirstName))
            {
                Students[index].FirstName = studentEntity.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(studentEntity.LastName))
            {
                Students[index].LastName = studentEntity.LastName;
            }

            return Json(studentEntity);
        }
    }
}
