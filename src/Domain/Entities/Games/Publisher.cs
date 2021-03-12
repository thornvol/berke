using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Domain.Entities.Games
{
    public class Publisher : BaseEntity
    {
        public Publisher()
        {
            Games = new List<Game>();
        }

        public int PublisherId { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
