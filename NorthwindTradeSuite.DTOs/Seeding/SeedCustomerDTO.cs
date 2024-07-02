using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs.Seeding.SharedSeedingDTOs;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedCustomerDTO : IMapTo<Customer>
    {
        [Name("customerID")]
        public string Id { get; set; } = null!;

        public SeedProfessionalDataDTO SeedProfessionalDataDTO { get; set; } = null!;

        public SeedLocationDataDTO SeedLocationDataDTO { get; set; } = null!;

        public SeedPersonalContactDataDTO SeedPersonalContactDataDTO { get; set; } = null!; 
    }
}
