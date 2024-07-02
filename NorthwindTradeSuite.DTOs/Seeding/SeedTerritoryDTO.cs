using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedTerritoryDTO : IMapTo<Territory>
    {
        [Name("territoryID")]
        public string Id { get; set; } = null!;

        [Name("territoryDescription")]
        public string Description { get; set; } = null!;

        [Name("regionID")]
        public string RegionId { get; set; } = null!;
    }
}
