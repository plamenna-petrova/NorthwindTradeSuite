using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<Category, string>
    {
        public override void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(c => c.Id)
                .HasColumnName(CATEGORY_ID_COLUMN);

            entityTypeBuilder
                .Property(c => c.Name)
                .HasColumnName(CATEGORY_NAME_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CATEGORY_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(c => c.Description)
                .HasColumnName(CATEGORY_DESCRIPTION_COLUMN)
                .HasColumnType("ntext")
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CATEGORY_DESCRIPTION_MAX_LENGTH);

            entityTypeBuilder
                .Property(c => c.Picture)
                .HasColumnName(CATEGORY_PICTURE_COLUMN)
                .HasColumnType("image")
                .IsRequired();

            entityTypeBuilder
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
