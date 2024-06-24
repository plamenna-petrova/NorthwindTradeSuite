using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedEmployeeTerritoryDTO
    {
        [Name("employeeID")]
        public string EmployeeId { get; set; } = null!;

        [Name("territoryID")]
        public string TerritoryId { get; set; } = null!;
    }
}
