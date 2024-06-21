using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedTerritoryDTO
    {
        [Name("territoryID")]
        public string Id { get; set; } = null!;

        [Name("territoryDescription")]
        public string Description { get; set; } = null!;

        [Name("regionID")]
        public string RegionId { get; set; } = null!;
    }
}
