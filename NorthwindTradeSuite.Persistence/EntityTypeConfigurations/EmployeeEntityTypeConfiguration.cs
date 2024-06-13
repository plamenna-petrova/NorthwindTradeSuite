using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Interfaces;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.EmployeeConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class EmployeeEntityTypeConfiguration : BaseEntityTypeConfiguration<Employee, string>
    {
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

            entityTypeBuilder
                .Property(emp => emp.Address)
                .HasColumnName(EMPLOYEE_ADDRESS_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_ADDRESS_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.City)
                .HasColumnName(EMPLOYEE_CITY_COLUMN)
                .IsRequired()
                .HasMaxLength(EMPLOYEE_CITY_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.Region)
                .HasColumnName(EMPLOYEE_REGION_COLUMN)
                .HasMaxLength(EMPLOYEE_REGION_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.PostalCode)
                .HasColumnName(EMPLOYEE_POSTAL_CODE_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(EMPLOYEE_POSTAL_CODE_MAX_LENGTH);

            entityTypeBuilder
                .Property(emp => emp.Country)
                .HasColumnName(EMPLOYEE_COUNTRY_COLUMN)
                .IsRequired()
                .HasConversion<string>()
                .IsUnicode(false)
                .HasMaxLength(EMPLOYEE_COUNTRY_MAX_LENGTH);

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
                .OnDelete(DeleteBehavior.NoAction);

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, GetCheckConstraintTableColumn(nameof(Employee), nameof(Employee.BirthDate))),
                $"{nameof(Employee.BirthDate)} > {nameof(IAuditInfo.CreatedAt)}"
            );

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, GetCheckConstraintTableColumn(nameof(Employee), nameof(Employee.HireDate))),
                $"{nameof(Employee.HireDate)} > {nameof(Employee.BirthDate)}"
            );
        }
    }
}
