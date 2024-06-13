using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<Category, string>
    {
        public override void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(cat => cat.Id)
                .HasColumnName(CATEGORY_ID_COLUMN);

            entityTypeBuilder
                .Property(cat => cat.Name)
                .HasColumnName(CATEGORY_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(CATEGORY_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(cat => cat.Description)
                .HasColumnName(CATEGORY_DESCRIPTION_COLUMN)
                .HasColumnType(NTEXT_COLUMN_TYPE)
                .IsRequired()
                .HasMaxLength(CATEGORY_DESCRIPTION_MAX_LENGTH);

            entityTypeBuilder
                .Property(cat => cat.Picture)
                .HasColumnName(CATEGORY_PICTURE_COLUMN)
                .HasColumnType(IMAGE_COLUMN_TYPE)
                .IsRequired();
        }
    }
}
