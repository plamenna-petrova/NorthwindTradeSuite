using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.ProductConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class ProductEntityTypeConfiguration : BaseEntityTypeConfiguration<Product, string>
    {
        public override void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(p => p.Id)
                .HasColumnName(PRODUCT_ID_COLUMN);

            entityTypeBuilder
                .Property(p => p.Name)
                .HasColumnName(PRODUCT_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(PRODUCT_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(p => p.SupplierId)
                .HasColumnName(PRODUCT_SUPPLIER_ID_COLUMN);

            entityTypeBuilder
                .Property(p => p.CategoryId)
                .HasColumnName(PRODUCT_CATEGORY_ID_COLUMN);

            entityTypeBuilder
                .Property(p => p.QuantityPerUnit)
                .HasColumnName(PRODUCT_QUANTITY_PER_UNIT_COLUMN)
                .IsRequired()
                .HasMaxLength(PRODUCT_QUANTITY_PER_UNIT_MAX_LENGTH);

            entityTypeBuilder
                .Property(p => p.UnitPrice)
                .HasColumnName(PRODUCT_UNIT_PRICE_COLUMN)
                .HasColumnType(MONEY_COLUMN_TYPE)
                .IsRequired(false)
                .HasDefaultValue(SQL_ZERO_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(p => p.UnitsInStock)
                .HasColumnName(PRODUCT_UNITS_IN_STOCK_COLUMN)
                .IsRequired(false)
                .HasDefaultValue(SQL_ZERO_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(p => p.UnitsInOrder)
                .HasColumnName(PRODUCT_UNITS_IN_ORDER_COLUMN)
                .IsRequired(false)
                .HasDefaultValue(SQL_ZERO_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(p => p.ReorderLevel)
                .HasColumnName(PRODUCT_REORDER_LEVEL_COLUMN)
                .IsRequired(false)
                .HasDefaultValue(SQL_ZERO_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(p => p.Discontinued)
                .HasColumnName(PRODUCT_DISCONTINUED_COLUMN)
                .IsRequired(true);

            entityTypeBuilder
                .HasOne(p => p.Supplier)
                .WithMany(sup => sup.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(PRODUCT_SUPPLIER_CONSTAINT_NAME);
        }
    }
}
