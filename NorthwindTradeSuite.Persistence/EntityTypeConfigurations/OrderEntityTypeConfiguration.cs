using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.OrderConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order, string>
    {
        public override void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(o => o.Id)
                .HasColumnName(ORDER_ID_COLUMN);

            entityTypeBuilder
                .Property(o => o.CustomerId)
                .HasColumnName(ORDER_CUSTOMER_ID_COLUMN);

            entityTypeBuilder
                .Property(o => o.EmployeeId)
                .HasColumnName(ORDER_EMPLOYEE_ID_COLUMN);

            entityTypeBuilder
                .Property(o => o.OrderDate)
                .HasColumnName(ORDER_DATE_COLUMN)
                .HasColumnType(DATETIME_COLUMN_TYPE)
                .IsRequired(false);

            entityTypeBuilder
                .Property(o => o.RequiredDate)
                .HasColumnName(ORDER_REQUIRED_DATE_COLUMN)
                .HasColumnType(DATETIME_COLUMN_TYPE)
                .IsRequired(false);

            entityTypeBuilder
                .Property(o => o.ShippedDate)
                .HasColumnName(ORDER_SHIPPED_DATE_COLUMN)
                .HasColumnType(DATETIME_COLUMN_TYPE)
                .IsRequired(false);

            entityTypeBuilder
                .Property(o => o.ShipperId)
                .HasColumnName(ORDER_SHIPPED_VIA_COLUMN);

            entityTypeBuilder
                .Property(o => o.Freight)
                .HasColumnName(ORDER_FREIGHT_COLUMN)
                .HasColumnType(MONEY_COLUMN_TYPE)
                .HasDefaultValueSql(SQL_ZERO_DEFAULT_VALUE);

            entityTypeBuilder
                .Property(o => o.ShipName)
                .HasColumnName(ORDER_SHIP_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(o => o.ShipAddress)
                .HasColumnName(ORDER_SHIP_ADDRESS_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_ADDRESS_MAX_LENGTH);

            entityTypeBuilder
                .Property(o => o.ShipCity)
                .HasColumnName(ORDER_SHIP_CITY_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_CITY_MAX_LENGTH);

            entityTypeBuilder
                .Property(o => o.ShipRegion)
                .HasColumnName(ORDER_SHIP_REGION_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_REGION_MAX_LENGTH);

            entityTypeBuilder
                .Property(o => o.ShipPostalCode)
                .HasColumnName(ORDER_SHIP_POSTAL_CODE_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_POSTAL_CODE_MAX_LENGTH);

            entityTypeBuilder
                .Property(o => o.ShipCountry)
                .HasColumnName(ORDER_SHIP_COUNTRY_COLUMN)
                .IsRequired()
                .HasMaxLength(ORDER_SHIP_COUNTRY_MAX_LENGTH);

            entityTypeBuilder
                .HasOne(o => o.Customer)
                .WithMany(cust => cust.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName(ORDER_CUSTOMER_CONSTAINT_NAME)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder
                .HasOne(o => o.Employee)
                .WithMany(emp => emp.Orders)
                .HasForeignKey(o => o.EmployeeId)
                .HasConstraintName(ORDER_EMPLOYEE_CONSTAINT_NAME)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder
                .HasOne(o => o.Shipper)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShipperId)
                .HasConstraintName(ORDER_SHIPPER_CONSTAINT_NAME)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
