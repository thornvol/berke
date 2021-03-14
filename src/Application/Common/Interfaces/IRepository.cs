using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Application.Common.Interfaces
{
    /// <summary>
    /// Common contract for repositories - decided not to use.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<in T> where T : class
    {
        Task Add(T entity);

        Task AddRange(T[] entities);

        Task Delete(T entity);
    }
}
