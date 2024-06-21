using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs
{
    public class SeedPersonalContactDataDTO
    {
        [Name("phone")]
        public string Phone { get; set; } = null!;

        [Name("fax")]
        public string Fax { get; set; } = null!;
    }
}
