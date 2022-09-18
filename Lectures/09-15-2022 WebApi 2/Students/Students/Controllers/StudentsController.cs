using Microsoft.AspNetCore.Mvc;
using Students.Entities;
using Students.Models;
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
        private static List<StudentModel> Students { get; set; } = new List<StudentModel>();

        [HttpGet]
        public StudentsEntity Get()
        {
            return new StudentsEntity()
            {
                Students = Students.Select(studentModel => new StudentEntity(studentModel))
            };
        }

        [HttpGet("{index:int}")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Json(new StudentEntity(Students[index]));
        }

        [HttpGet("{index:int}/admin")]
        public IActionResult GetAdmin(int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Json(new AdminStudentEntity(Students[index]));
        }

        [HttpPost]
        public StudentEntity Post([FromBody] StudentEntity studentEntity)
        {
            Students.Add(studentEntity.ToModel());

            return studentEntity;
        }

        [HttpPut("{index:int}")]
        public IActionResult Put([FromBody] StudentEntity studentEntity, int index)
        {
            if (index < 0 || index >= Students.Count)
            {
                return StatusCode((int) HttpStatusCode.NotFound);
            }

            Students[index] = studentEntity.ToModel();

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
