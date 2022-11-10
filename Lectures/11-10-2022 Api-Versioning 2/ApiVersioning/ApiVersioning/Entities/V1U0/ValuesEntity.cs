using ApiVersioning.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiVersioning.Entities.V1U0
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
        }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Value { get; set; }


        public ValuesModel ToModel()
        {
            return new ValuesModel()
            {
                Name = this.Name,
                Value = this.Value
            };
        }
    }
}
