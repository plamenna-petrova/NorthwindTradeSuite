using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedSupplierDTO
    {
        [Name("supplierID")]
        public string Id { get; set; } = null!;

        public SeedProfessionalDataDTO SeedProfessionalDataDTO { get; set; } = null!;

        public SeedLocationDataDTO SeedLocationDataDTO { get; set; } = null!;

        public SeedPersonalContactDataDTO SeedPersonalContactDataDTO { get; set; } = null!;

        [Name("homePage")]
        public string HomePage { get; set; } = null!;
    }
}
