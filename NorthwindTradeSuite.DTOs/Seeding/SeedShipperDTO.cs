using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedShipperDTO : IMapTo<Shipper>
    {
        [Name("shipperID")]
        public string Id { get; set; } = null!;

        [Name("companyName")]
        public string CompanyName { get; set; } = null!;

        [Name("phone")]
        public string Phone { get; set; } = null!;
    }
}
