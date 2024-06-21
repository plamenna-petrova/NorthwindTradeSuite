using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedCustomerDTO
    {
        [Name("customerID")]
        public string Id { get; set; } = null!;

        public SeedProfessionalDataDTO SeedProfessionalDataDTO { get; set; } = null!;

        public SeedLocationDataDTO SeedLocationDataDTO { get; set; } = null!;

        public SeedPersonalContactDataDTO SeedPersonalContactDataDTO { get; set; } = null!; 
    }
}
