using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.OrderDetailsConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class OrderDetailsEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable(ORDER_DETAILS_TABLE_NAME);

            entityTypeBuilder
                .HasKey(od => new { od.OrderId, od.ProductId })
                .IsClustered(false);

            entityTypeBuilder
                .Property(od => od.OrderId)
                .HasColumnName(ORDER_ID_COLUMN);

            entityTypeBuilder
                .Property(od => od.ProductId)
                .HasColumnName(PRODUCT_ID_COLUMN);

            entityTypeBuilder
                .Property(od => od.UnitPrice)
                .HasColumnName(ORDER_DETAILS_UNIT_PRICE_COLUMN)
                .HasColumnType(MONEY_COLUMN_TYPE)
                .IsRequired();

            entityTypeBuilder
                .Property(od => od.Quantity)
                .HasColumnName(ORDER_DETAILS_QUANTITY_COLUMN)
                .IsRequired()
                .HasDefaultValueSql(SQL_ONE_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(od => od.Discount)
                .HasColumnName(ORDER_DETAILS_DISCOUNT_COLUMN)
                .IsRequired();

            entityTypeBuilder
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .HasConstraintName(ORDERS_CONSTAINT_NAME)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .HasConstraintName(PRODUCTS_CONSTAINT_NAME)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
