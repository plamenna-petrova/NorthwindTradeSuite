using CsvHelper.Configuration.Attributes;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedShipperDTO
    {
        [Name("shipperID")]
        public string Id { get; set; } = null!;

        [Name("companyName")]
        public string CompanyName { get; set; } = null!;

        [Name("phone")]
        public string Phone { get; set; } = null!;
    }
}
