using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString()[..8];
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
            Claims = new HashSet<IdentityUserClaim<string>>();
            Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string RefreshToken { get; set; } = null!;

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
