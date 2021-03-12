using System.Threading;
using System.Threading.Tasks;
using BerkeGaming.Domain.Entities;
using BerkeGaming.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Game> Games { get; set; }

        DbSet<Genre> Genres { get; set; }

        DbSet<Publisher> Publishers { get; set; }

        DbSet<UserGame> UserGames { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
