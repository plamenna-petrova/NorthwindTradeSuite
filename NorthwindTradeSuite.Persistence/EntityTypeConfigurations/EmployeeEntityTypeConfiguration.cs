using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Contracts;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Common;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.EmployeeConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class EmployeeEntityTypeConfiguration : BaseEntityTypeConfiguration<Employee, string>
    {
        private static readonly string[] EmployeeLocationDataColumns =
        {
            EMPLOYEE_ADDRESS_COLUMN,
            EMPLOYEE_CITY_COLUMN,
            EMPLOYEE_REGION_COLUMN,
            EMPLOYEE_POSTAL_CODE_COLUMN,
            EMPLOYEE_COUNTRY_COLUMN
        };

        private static readonly int[] EmployeeLocationDataMaxLengthConstraints =
        {
            EMPLOYEE_ADDRESS_MAX_LENGTH,
            EMPLOYEE_CITY_MAX_LENGTH,
            EMPLOYEE_REGION_MAX_LENGTH,
            EMPLOYEE_POSTAL_CODE_MAX_LENGTH,
            EMPLOYEE_COUNTRY_MAX_LENGTH
        };

        public override void Configure(EntityTypeBuilder<Employee> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(emp => emp.Id)
                .HasColumnName(EMPLOYEE_ID_COLUMN);

            entityTypeBuilder
                .Property(emp => emp.LastName)
                .HasColumnName(EMPLOYEE_LAST_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_LAST_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.FirstName)
                .HasColumnName(EMPLOYEE_FIRST_NAME_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_FIRST_NAME_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.Title)
                .HasColumnName(EMPLOYEE_TITLE_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_TITLE_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.TitleOfCourtesy)
                .HasColumnName(EMPLOYEE_TITLE_OF_COURTESY_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_TITLE_OF_COURTESY_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.BirthDate)
                .HasColumnName(EMPLOYEE_BIRTHDATE_COLUMN)
                .HasColumnType(DATETIME_COLUMN_TYPE)
                .IsRequired(false);

            entityTypeBuilder
                .Property(emp => emp.HireDate)
                .HasColumnName(EMPLOYEE_HIRE_DATE_COLUMN)
                .HasColumnType(DATETIME_COLUMN_TYPE)
                .IsRequired(false);

            entityTypeBuilder.OwnsOne(
                emp => emp.LocationData,
                ownedNavigationBuilder => OwnedNavigationConfigurator.ConfigureLocationData(
                    ownedNavigationBuilder,
                    EmployeeLocationDataColumns,
                    EmployeeLocationDataMaxLengthConstraints
                )
            );

            entityTypeBuilder
                .Property(emp => emp.HomePhone)
                .HasColumnName(EMPLOYEE_HOME_PHONE_COLUMN)
                .IsRequired(false)
                .HasMaxLength(EMPLOYEE_HOME_PHONE_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.Extension)
                .HasColumnName(EMPLOYEE_EXTENSION_COLUMN)
                .IsRequired(false)
                .HasMaxLength(EMPLOYEE_EXTENSION_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.Photo)
                .HasColumnName(EMPLOYEE_PHOTO_COLUMN)
                .HasColumnType(IMAGE_COLUMN_TYPE)
                .IsRequired();

            entityTypeBuilder
                .Property(emp => emp.Notes)
                .HasColumnName(EMPLOYEE_NOTES_COLUMN)
                .HasColumnType(NTEXT_COLUMN_TYPE)
                .IsRequired(false)
                .HasMaxLength(EMPLOYEE_NOTES_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.PhotoPath)
                .HasColumnName(EMPLOYEE_PHOTO_PATH_COLUMN)
                .IsRequired(false)
                .HasMaxLength(EMPLOYEE_PHOTO_PATH_MAX_LENGTH);

            entityTypeBuilder
                .HasOne(emp => emp.Manager)
                .WithMany(emp => emp.DirectReports)
                .HasForeignKey(emp => emp.ReportsTo)
                .HasConstraintName(EMPLOYEE_REPORTS_CONSTRAINT_NAME)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, GetCheckConstraintTableColumn(nameof(Employee), nameof(Employee.BirthDate))),
                $"{nameof(IAuditInfo.CreatedAt)} IS NULL OR {nameof(Employee.BirthDate)} < {nameof(IAuditInfo.CreatedAt)}"
            );

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, GetCheckConstraintTableColumn(nameof(Employee), nameof(Employee.HireDate))),
                $"{nameof(Employee.HireDate)} > {nameof(Employee.BirthDate)}"
            );
        }
    }
}
