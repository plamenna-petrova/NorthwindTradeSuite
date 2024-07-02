using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedEmployeeTerritoryDTO : IMapTo<EmployeeTerritory>
    {
        [Name("employeeID")]
        public string EmployeeId { get; set; } = null!;

        [Name("territoryID")]
        public string TerritoryId { get; set; } = null!;
    }
}
