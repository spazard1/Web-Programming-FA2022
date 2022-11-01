using Gargoyles.Models;

namespace Gargoyles.Services
{
    public class GargoylesDatabase
    {

        private readonly Dictionary<string, GargoyleModel> gargoyles = new();


        public void AddOrReplace(GargoyleModel model)
        {
            model.Updated = DateTime.UtcNow;
            this.gargoyles[model.Name] = model;
        }

        public GargoyleModel Get(string name)
        {
            // should return null if this doesn't exist, not throw an exception.
            return this.gargoyles[name];
        }


    }
}
