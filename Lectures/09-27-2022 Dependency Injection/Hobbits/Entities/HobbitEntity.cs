using Hobbits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hobbits.Entities
{
    public class HobbitEntity
    {
        /// <summary>
        /// Entity classes must have a no-args constructor
        /// </summary>
        public HobbitEntity()
        {

        }

        public HobbitEntity(HobbitModel hobbitModel)
        {
            this.Name = hobbitModel.Name;
            this.Group = hobbitModel.Group;
        }

        public string Name { get; set; }

        public string Group { get; set; }

        public HobbitModel ToModel()
        {
            return new HobbitModel()
            {
                Name = this.Name,
                Group = this.Group
            };
        }
    }
}
