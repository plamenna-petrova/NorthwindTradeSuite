using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Common
{
    internal static class OwnedNavigationConfigurator 
    {
        internal static void ConfigureProfessionalData<TEntity>(
            OwnedNavigationBuilder<TEntity, ProfessionalData> ownedNavigationBuilder, 
            string[] professionalDataColumns, 
            int[] professionalDataPropertiesMaxLengthConstraints) where TEntity : class
        {
            ownedNavigationBuilder.WithOwner();

            ownedNavigationBuilder
                .Property(pd => pd.CompanyName)
                .HasColumnName(professionalDataColumns[0])
                .IsRequired()
                .HasMaxLength(professionalDataPropertiesMaxLengthConstraints[0]);

            ownedNavigationBuilder
                .Property(pd => pd.ContactName)
                .HasColumnName(professionalDataColumns[1])
                .IsRequired()
                .HasMaxLength(professionalDataPropertiesMaxLengthConstraints[1]);

            ownedNavigationBuilder
                .Property(pd => pd.ContactTitle)
                .HasColumnName(professionalDataColumns[2])
                .IsRequired()
                .HasMaxLength(professionalDataPropertiesMaxLengthConstraints[2]);
        }

        internal static void ConfigureLocationData<TEntity>(
            OwnedNavigationBuilder<TEntity, LocationData> ownedNavigationBuilder,
            string[] locationDataColumns,
            int[] locationDataPropertiesMaxLengthConstraints) where TEntity : class
        {
            ownedNavigationBuilder.WithOwner();

            ownedNavigationBuilder
                .Property(ld => ld.Address)
                .HasColumnName(locationDataColumns[0])
                .IsRequired()
                .HasMaxLength(locationDataPropertiesMaxLengthConstraints[0]);

            ownedNavigationBuilder
                .Property(ld => ld.City)
                .HasColumnName(locationDataColumns[1])
                .IsRequired()
                .HasMaxLength(locationDataPropertiesMaxLengthConstraints[1]);

            ownedNavigationBuilder
                .Property(ld => ld.Region)
                .HasColumnName(locationDataColumns[2])
                .IsRequired()
                .HasMaxLength(locationDataPropertiesMaxLengthConstraints[2]);

            ownedNavigationBuilder
                .Property(ld => ld.PostalCode)
                .HasColumnName(locationDataColumns[3])
                .IsRequired()
                .HasMaxLength(locationDataPropertiesMaxLengthConstraints[3]);

            ownedNavigationBuilder
                .Property(ld => ld.Country)
                .HasColumnName(locationDataColumns[4])
                .IsRequired()
                .HasMaxLength(locationDataPropertiesMaxLengthConstraints[4]);
        }

        internal static void ConfigurePersonalContactData<TEntity>(
            OwnedNavigationBuilder<TEntity, PersonalContactData> ownedNavigationBuilder,
            string[] personalContactDataColumns,
            int[] personalContactDataPropertiesMaxLengthConstraints) where TEntity : class
        {
            ownedNavigationBuilder.WithOwner();

            ownedNavigationBuilder
                .Property(pcd => pcd.Phone)
                .HasColumnName(personalContactDataColumns[0])
                .IsRequired()
                .HasMaxLength(personalContactDataPropertiesMaxLengthConstraints[0]);

            ownedNavigationBuilder
                .Property(pcd => pcd.Fax)
                .HasColumnName(personalContactDataColumns[1])
                .IsRequired(false)
                .HasMaxLength(personalContactDataPropertiesMaxLengthConstraints[1]);
        }
    }
}
