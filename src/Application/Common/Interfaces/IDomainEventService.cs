using System.Threading.Tasks;
using BerkeGaming.Domain.Common;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
