using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Mappings;
using BerkeGaming.Domain.Entities.Games;

namespace BerkeGaming.Application.Common.Models
{
    public class GenreDto : IMapFrom<Genre>
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        // Exclude list of games for now.
        //public IList<GameDto> Games { get; set; }
    }
}
