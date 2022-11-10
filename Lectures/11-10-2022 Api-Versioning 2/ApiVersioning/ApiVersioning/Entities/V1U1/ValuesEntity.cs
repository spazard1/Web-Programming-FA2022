using ApiVersioning.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiVersioning.Entities.V1U1
{
    public class ValuesEntity
    {
        public ValuesEntity()
        {

        }

        public ValuesEntity(ValuesModel model)
        {
            this.Name = model.Name;
            this.Value = model.Value;
            this.Description = model.Description;
        }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Value { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        public ValuesModel ToModel()
        {
            return new ValuesModel()
            {
                Name = this.Name,
                Value = this.Value,
                Description = this.Description
            };
        }
    }
}
