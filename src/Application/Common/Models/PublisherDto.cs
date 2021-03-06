using BerkeGaming.Application.Common.Mappings;
using BerkeGaming.Domain.Entities.Games;

namespace BerkeGaming.Application.Common.Models
{
    public class PublisherDto : IMapFrom<Publisher>
    {
        public int PublisherId { get; set; }

        public string Name { get; set; }

        //public IList<GameDto> Games { get; set; }
    }
}
