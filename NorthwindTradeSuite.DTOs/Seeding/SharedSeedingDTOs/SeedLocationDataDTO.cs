using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs
{
    public class SeedLocationDataDTO
    {
        [Name("address")]
        public string Address { get; set; } = null!;

        [Name("city")]
        public string City { get; set; } = null!;

        [Name("region")]
        public string Region { get; set; } = null!;

        [Name("postalCode")]
        public string PostalCode { get; set; } = null!;

        [Name("country")]
        public string Country { get; set; } = null!;
    }
}
