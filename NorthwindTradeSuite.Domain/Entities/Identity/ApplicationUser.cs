using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 7);
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
            Claims = new HashSet<IdentityUserClaim<string>>();
            Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
