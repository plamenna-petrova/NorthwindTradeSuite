using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.ShipperConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class ShipperEntityTypeConfiguration : BaseEntityTypeConfiguration<Shipper, string>
    {
        public override void Configure(EntityTypeBuilder<Shipper> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(ship => ship.Id)
                .HasColumnName(SHIPPER_ID_COLUMN);

            entityTypeBuilder
                .Property(ship => ship.CompanyName)
                .HasColumnName(SHIPPER_COMPANY_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(SHIPPER_COMPANY_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(ship => ship.Phone)
                .HasColumnName(SHIPPER_PHONE_COLUMN)
                .IsRequired()
                .HasMaxLength(SHIPPER_PHONE_MAX_LENGTH);
        }
    }
}
