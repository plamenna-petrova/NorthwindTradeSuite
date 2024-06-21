using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs
{
    public class SeedProfessionalDataDTO
    {
        [Name("companyName")]
        public string CompanyName { get; set; } = null!;

        [Name("contactName")]
        public string ContactName { get; set; } = null!;

        [Name("contactTitle")]
        public string ContactTitle { get; set; } = null!;
    }
}
