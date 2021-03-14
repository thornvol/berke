using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BerkeGaming.Application.Common.Interfaces
{
    public interface IUnityOfWork
    {
        Task Commit(CancellationToken ctx = default);
    }
}
