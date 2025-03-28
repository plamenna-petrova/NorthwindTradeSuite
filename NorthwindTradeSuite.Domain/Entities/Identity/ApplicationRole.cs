﻿using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole, IAuditInfo
    {
        public ApplicationRole(string name) : base(name)
        {
            Id = Guid.NewGuid().ToString()[..8];
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
