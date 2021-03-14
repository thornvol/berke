using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BerkeGaming.Application.Common.Interfaces;

namespace BerkeGaming.Infrastructure.Persistence
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit changes to the database.
        /// </summary>
        /// <param name="ctx">An optional cancellation token.</param>
        /// <returns></returns>
        public async Task Commit(CancellationToken ctx = default)
        {
            await _context.SaveChangesAsync(ctx);
        }
    }
}
