using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.EmployeeTerritoryConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public sealed class EmployeeTerritoryEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeTerritory>
    {
        public void Configure(EntityTypeBuilder<EmployeeTerritory> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(et => new { et.EmployeeId, et.TerritoryId })
                .IsClustered(false);

            entityTypeBuilder
                .Property(et => et.EmployeeId)
                .HasColumnName(EMPLOYEE_ID_COLUMN);

            entityTypeBuilder
                .Property(et => et.TerritoryId)
                .HasColumnName(TERRITORY_ID_COLUMN);

            entityTypeBuilder
                .HasOne(et => et.Employee)
                .WithMany(emp => emp.EmployeeTerritories)
                .HasForeignKey(et => et.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(EMPLOYEES_CONSTRAINT_NAME);

            entityTypeBuilder
                .HasOne(et => et.Territory)
                .WithMany(t => t.EmployeeTerritories)
                .HasForeignKey(et => et.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(TERRITORIES_CONSTRAINT_NAME);
        }
    }
}
