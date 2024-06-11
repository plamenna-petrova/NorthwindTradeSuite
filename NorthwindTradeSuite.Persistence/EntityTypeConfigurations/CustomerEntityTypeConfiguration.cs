using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CustomerConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class CustomerEntityTypeConfiguration : BaseEntityTypeConfiguration<Customer, string>
    {
        public override void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(cust => cust.Id)
                .HasColumnName(CUSTOMER_ID_COLUMN);

            entityTypeBuilder
                .Property(cust => cust.CompanyName)
                .HasColumnName(CUSTOMER_COMPANY_NAME_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_COMPANY_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.ContactName)
                .HasColumnName(CUSTOMER_CONTACT_NAME_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_CONTACT_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.ContactTitle)
                .HasColumnName(CUSTOMER_CONTACT_TITLE_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_CONTACT_TITLE_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.Address)
                .HasColumnName(CUSTOMER_ADDRESS_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_ADDRESS_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.City)
                .HasColumnName(CUSTOMER_CITY_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_CITY_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.Region)
                .HasColumnName(CUSTOMER_REGION_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_REGION_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.PostalCode)
                .HasColumnName(CUSTOMER_POSTAL_CODE_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_POSTAL_CODE_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.Country)
                .HasColumnName(CUSTOMER_COUNTRY_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_COUNTRY_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.Phone)
                .HasColumnName(CUSTOMER_PHONE_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_PHONE_MAX_LENGTH);

            entityTypeBuilder
                .Property(cust => cust.Fax)
                .HasColumnName(CUSTOMER_FAX_COLUMN)
                .IsRequired(false)
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(CUSTOMER_FAX_MAX_LENGTH);

            entityTypeBuilder
                .HasMany(cust => cust.Orders)
                .WithOne(o => o.Customer)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
