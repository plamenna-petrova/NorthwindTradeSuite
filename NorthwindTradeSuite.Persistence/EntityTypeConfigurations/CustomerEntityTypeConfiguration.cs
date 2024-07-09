using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Common;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CustomerConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class CustomerEntityTypeConfiguration : BaseEntityTypeConfiguration<Customer>
    {
        private static readonly string[] CustomerProfessionalDataColumns = 
        { 
            CUSTOMER_COMPANY_NAME_COLUMN, 
            CUSTOMER_CONTACT_NAME_COLUMN, 
            CUSTOMER_CONTACT_TITLE_COLUMN 
        };

        private static readonly int[] CustomerProfessionalDataMaxLengthConstraints =
        {
            CUSTOMER_COMPANY_NAME_MAX_LENGTH, 
            CUSTOMER_CONTACT_NAME_MAX_LENGTH, 
            CUSTOMER_CONTACT_TITLE_MAX_LENGTH
        };

        private static readonly string[] CustomerLocationDataColumns =
        {
            CUSTOMER_ADDRESS_COLUMN,
            CUSTOMER_CITY_COLUMN,
            CUSTOMER_REGION_COLUMN,
            CUSTOMER_POSTAL_CODE_COLUMN,
            CUSTOMER_COUNTRY_COLUMN
        };

        private static readonly int[] CustomerLocationDataMaxLengthConstraints =
        {
            CUSTOMER_ADDRESS_MAX_LENGTH,
            CUSTOMER_CITY_MAX_LENGTH,
            CUSTOMER_REGION_MAX_LENGTH,
            CUSTOMER_POSTAL_CODE_MAX_LENGTH,
            CUSTOMER_COUNTRY_MAX_LENGTH
        };

        private static readonly string[] CustomerPersonalContactDataColumns =
        {
            CUSTOMER_PHONE_COLUMN,
            CUSTOMER_FAX_COLUMN
        };

        private static readonly int[] CustomerPersonalContactDataMaxLengthConstraints =
        {
            CUSTOMER_PHONE_MAX_LENGTH,
            CUSTOMER_FAX_MAX_LENGTH
        };

        public override void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(cust => cust.Id)
                .HasColumnName(CUSTOMER_ID_COLUMN);

            entityTypeBuilder.OwnsOne(
                cust => cust.ProfessionalData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigureProfessionalData(
                    ownedNavigationBuilder,
                    CustomerProfessionalDataColumns,
                    CustomerProfessionalDataMaxLengthConstraints
                )
            );

            entityTypeBuilder.OwnsOne(
                cust => cust.LocationData, 
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigureLocationData(
                    ownedNavigationBuilder,
                    CustomerLocationDataColumns,
                    CustomerLocationDataMaxLengthConstraints
                )
            );

            entityTypeBuilder.OwnsOne(
                cust => cust.PersonalContactData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigurePersonalContactData(
                    ownedNavigationBuilder,
                    CustomerPersonalContactDataColumns,
                    CustomerPersonalContactDataMaxLengthConstraints
                )
            );
        }
    }
}
