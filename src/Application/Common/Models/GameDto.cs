using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BerkeGaming.Application.Common.Mappings;
using BerkeGaming.Domain.Entities.Games;

namespace BerkeGaming.Application.Common.Models
{
    public class GameDto : IMapFrom<Game>
    {
        public int GameId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string Overview { get; set; }

        public PublisherDto Publisher { get; set; }

        public IList<GenreDto> Genres { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Game, GameDto>();
        }
    }
}
