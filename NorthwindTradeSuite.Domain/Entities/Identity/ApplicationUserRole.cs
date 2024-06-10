using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities.Identity
{
    public class ApplicationUserRole
    {
        public virtual ApplicationRole Role { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
