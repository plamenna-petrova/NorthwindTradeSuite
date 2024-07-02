using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedCategoryDTO : IMapTo<Category>
    {
        [Name("categoryID")]
        public string Id { get; set; } = null!;

        [Name("categoryName")]
        public string Name { get; set; } = null!;

        [Name("description")]
        public string Description { get; set; } = null!;

        [Name("picture")]
        public byte[] Picture { get; set; } = null!;
    }
}
