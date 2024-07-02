using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedRegionDTO : IMapTo<Region>
    {
        [Name("regionID")]
        public string Id { get; set; } = null!;

        [Name("regionDescription")]
        public string Description { get; set; } = null!;
    }
}
