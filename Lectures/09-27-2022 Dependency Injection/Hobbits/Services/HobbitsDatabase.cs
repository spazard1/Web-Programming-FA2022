using Hobbits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hobbits.Services
{
    public class HobbitsDatabase
    {
        private readonly List<HobbitModel> hobbits = new List<HobbitModel>()
        {
            new HobbitModel() { Name = "Frodo", Group = "hobbits" },
            new HobbitModel() { Name = "Sam", Group = "hobbits" },
            new HobbitModel() { Name = "Merry", Group = "hobbits" },
            new HobbitModel() { Name = "Pippin", Group = "hobbits" }
        };

        public int Count
        {
            get => hobbits.Count;
        }

        public IEnumerable<HobbitModel> GetAll()
        {
            return hobbits;
        }

        public HobbitModel Get(int id)
        {
            return hobbits[id];
        }

        public void Add(HobbitModel hobbitModel)
        {
            hobbits.Add(hobbitModel);
        }
    }
}
