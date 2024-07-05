using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities.Identity;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Identity
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(au => au.RefreshToken)
                .HasColumnName(REFRESH_TOKEN_COLUMN)
                .IsRequired(false);

            entityTypeBuilder
                .Property(au => au.RefreshTokenExpiryTime)
                .HasColumnName(REFRESH_TOKEN_EXPIRY_TIME_COLUMN)
                .IsRequired(false);

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
