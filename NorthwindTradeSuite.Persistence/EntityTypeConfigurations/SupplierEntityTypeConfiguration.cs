using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.SupplierConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class SupplierEntityTypeConfiguration : BaseEntityTypeConfiguration<Supplier, string>
    {
        public override void Configure(EntityTypeBuilder<Supplier> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(sup => sup.Id)
                .HasColumnName(SUPPLIER_ID_COLUMN);

            entityTypeBuilder
                .Property(sup => sup.CompanyName)
                .HasColumnName(SUPPLIER_COMPANY_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_COMPANY_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.ContactName)
                .HasColumnName(SUPPLIER_CONTACT_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_CONTACT_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.ContactTitle)
                .HasColumnName(SUPPLIER_CONTACT_TITLE_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_CONTACT_TITLE_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.Address)
                .HasColumnName(SUPPLIER_ADDRESS_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_ADDRESS_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.City)
                .HasColumnName(SUPPLIER_CITY_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_CITY_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.Region)
                .HasColumnName(SUPPLIER_REGION_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_REGION_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.PostalCode)
                .HasColumnName(SUPPLIER_POSTAL_CODE_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_POSTAL_CODE_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.Country)
                .HasColumnName(SUPPLIER_COUNTRY_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_COUNTRY_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.Phone)
                .HasColumnName(SUPPLIER_PHONE_COLUMN)
                .IsRequired()
                .HasMaxLength(SUPPLIER_PHONE_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.Fax)
                .HasColumnName(SUPPLIER_FAX_COLUMN)
                .IsRequired(false)
                .HasMaxLength(SUPPLIER_FAX_MAX_LENGTH);

            entityTypeBuilder
                .Property(sup => sup.HomePage)
                .HasColumnName(SUPPLIER_HOME_PAGE_COLUMN)
                .HasColumnType(NTEXT_COLUMN_TYPE)
                .IsRequired(false);
        }
    }
}
