using System;
using System.ComponentModel.DataAnnotations;

namespace FinalExam.Models
{
    public class BoardGameModel
    {
        public BoardGameModel()
        {
            this.Updated = DateTime.Now;
        }

        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(3)]
        public string Genre { get; set; }

        [Range(1, 10)]
        public int NumberOfPlayers { get; set; }

        public DateTime Updated { get; set; }

        public string ETag
        {
            get { return Updated.ToString(); }
        }
    }
}
