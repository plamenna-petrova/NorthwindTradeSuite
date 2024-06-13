using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities.Identity;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Identity
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasMany(au => au.Claims)
                .WithOne()
                .HasForeignKey(iuc => iuc.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasMany(au => au.Logins)
                .WithOne()
                .HasForeignKey(iul => iul.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasMany(au => au.ApplicationUserRoles)
                .WithOne(aur => aur.User)
                .HasForeignKey(aur => aur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
