using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Common;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.SupplierConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class SupplierEntityTypeConfiguration : BaseEntityTypeConfiguration<Supplier, string>
    {
        private static readonly string[] SupplierProfessionalDataColumns =
        {
            SUPPLIER_COMPANY_NAME_COLUMN,
            SUPPLIER_CONTACT_NAME_COLUMN,
            SUPPLIER_CONTACT_TITLE_COLUMN
        };

        private static readonly int[] SupplierProfessionalDataMaxLengthConstraints =
        {
            SUPPLIER_COMPANY_NAME_MAX_LENGTH,
            SUPPLIER_CONTACT_NAME_MAX_LENGTH,
            SUPPLIER_CONTACT_TITLE_MAX_LENGTH
        };

        private static readonly string[] SupplierLocationDataColumns =
        {
            SUPPLIER_ADDRESS_COLUMN,
            SUPPLIER_CITY_COLUMN,
            SUPPLIER_REGION_COLUMN,
            SUPPLIER_POSTAL_CODE_COLUMN,
            SUPPLIER_COUNTRY_COLUMN
        };

        private static readonly int[] SupplierLocationDataMaxLengthConstraints =
        {
            SUPPLIER_ADDRESS_MAX_LENGTH,
            SUPPLIER_CITY_MAX_LENGTH,
            SUPPLIER_REGION_MAX_LENGTH,
            SUPPLIER_POSTAL_CODE_MAX_LENGTH,
            SUPPLIER_COUNTRY_MAX_LENGTH
        };

        private static readonly string[] SupplierPersonalContactDataColumns =
        {
            SUPPLIER_PHONE_COLUMN,
            SUPPLIER_FAX_COLUMN
        };

        private static readonly int[] SupplierPersonalContactDataMaxLengthConstraints =
        {
            SUPPLIER_PHONE_MAX_LENGTH,
            SUPPLIER_FAX_MAX_LENGTH
        };

        public override void Configure(EntityTypeBuilder<Supplier> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(sup => sup.Id)
                .HasColumnName(SUPPLIER_ID_COLUMN);

            entityTypeBuilder.OwnsOne(
                sup => sup.ProfessionalData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigureProfessionalData(
                    ownedNavigationBuilder,
                    SupplierProfessionalDataColumns,
                    SupplierProfessionalDataMaxLengthConstraints
                )
            );

            entityTypeBuilder.OwnsOne(
                sup => sup.LocationData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigureLocationData(
                    ownedNavigationBuilder,
                    SupplierLocationDataColumns,
                    SupplierLocationDataMaxLengthConstraints
                )
            );

            entityTypeBuilder.OwnsOne(
                sup => sup.PersonalContactData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigurePersonalContactData(
                    ownedNavigationBuilder,
                    SupplierPersonalContactDataColumns,
                    SupplierPersonalContactDataMaxLengthConstraints
                )
            );

            entityTypeBuilder
                .Property(sup => sup.HomePage)
                .HasColumnName(SUPPLIER_HOME_PAGE_COLUMN)
                .HasColumnType(NTEXT_COLUMN_TYPE)
                .IsRequired(false);
        }
    }
}
