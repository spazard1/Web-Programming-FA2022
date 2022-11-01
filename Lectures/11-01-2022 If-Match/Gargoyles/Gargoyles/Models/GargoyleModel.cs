namespace Gargoyles.Models
{
    public class GargoyleModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public DateTime Updated { get; set; }

        public string ETag()
        {
            return this.Updated.ToString();
        }
    }
}
