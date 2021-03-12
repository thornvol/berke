
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Domain.Entities.Games
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
