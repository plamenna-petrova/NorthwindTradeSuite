using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Domain.Interfaces;

namespace NorthwindTradeSuite.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole, IAuditInfo
    {
        public ApplicationRole(string name) : base(name)
        {
            Id = Guid.NewGuid().ToString()[..7];
        }

        public ApplicationRole() : this(null!)
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = null!;
    }
}
