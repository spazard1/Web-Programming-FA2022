using Gargoyles.Models;

namespace Gargoyles.Entities
{
    public class GargoyleEntity
    {
        public GargoyleEntity()
        {

        }

        public GargoyleEntity(GargoyleModel model)
        {
            this.Name = model.Name;
            this.Color = model.Color;
            this.Size = model.Size;
        }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }


        public GargoyleModel ToModel()
        {
            return new GargoyleModel()
            {
                Name = this.Name,
                Color = this.Color,
                Size = this.Size
            };
        }
    }
}
