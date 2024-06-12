using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.TerritoryConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class TerritoryEntityTypeConfiguration : BaseEntityTypeConfiguration<Territory, string>
    {
        public override void Configure(EntityTypeBuilder<Territory> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(t => t.Id)
                .HasColumnName(TERRITORY_ID_COLUMN);

            entityTypeBuilder
                .Property(t => t.Description)
                .HasColumnName(TERRITORY_DESCRIPTION_COLUMN)
                .IsRequired()
                .HasMaxLength(TERRITORY_DESCRIPTION_MAX_LENGTH);

            entityTypeBuilder
                .Property(t => t.RegionId)
                .HasColumnName(TERRITORY_REGION_ID_COLUMN);

            entityTypeBuilder
                .HasOne(t => t.Region)
                .WithMany(r => r.Territories)
                .HasForeignKey(t => t.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(TERRITORY_REGION_CONSTRAINT_NAME);
        }
    }
}
