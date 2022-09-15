using System.ComponentModel.DataAnnotations;

namespace Students.Entities
{
    public class StudentEntity
    {

        [MinLength(3)]
        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }
    }
}
