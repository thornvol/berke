using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Application.Common.Interfaces
{
    /// <summary>
    /// Common contract/interface for application users.
    /// </summary>
    public interface IApplicationUser
    {
        string UserName { get; set; }
        string PasswordHash { get; set; }
        bool IsAdministrator { get; set; }
    }
}
