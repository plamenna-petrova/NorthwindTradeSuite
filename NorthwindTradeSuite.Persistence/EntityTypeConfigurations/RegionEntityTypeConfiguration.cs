using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.RegionConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class RegionEntityTypeConfiguration : BaseEntityTypeConfiguration<Region, string>
    {
        public override void Configure(EntityTypeBuilder<Region> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(r => r.Id)
                .HasColumnName(REGION_ID_COLUMN)
                .ValueGeneratedNever();

            entityTypeBuilder
                .Property(r => r.Description)
                .HasColumnName(REGION_DESCRIPTION_COLUMN)
                .IsRequired()
                .HasMaxLength(REGION_DESCRIPTION_MAX_LENGTH);
        }
    }
}
