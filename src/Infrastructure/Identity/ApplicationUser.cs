using BerkeGaming.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BerkeGaming.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        /// <summary>
        /// Is the user an Administrator.
        /// Add new field to Asp.net's identity database.
        /// </summary>
        public bool IsAdministrator { get; set; }
    }
}
