using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkeGaming.Domain.Entities
{
    public class BaseEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedUserId { get; set; }
    }
}
