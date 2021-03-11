using System.Threading;
using System.Threading.Tasks;
using BerkeGaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
