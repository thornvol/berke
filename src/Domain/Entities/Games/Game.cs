using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Domain.Entities.Games
{
    public class Game : BaseEntity
    {
        public Game()
        {
            Genres = new List<Genre>();
        }

        public int GameId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string Overview { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }
    }
}
