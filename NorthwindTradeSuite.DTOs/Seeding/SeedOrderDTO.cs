using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs.Seeding.TypeConverters;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedOrderDTO : IMapTo<Order>
    {
        [Name("orderID")]
        public string Id { get; set; } = null!;

        [Name("customerID")]
        public string CustomerId { get; set; } = null!;

        [Name("employeeID")]
        public string EmployeeId { get; set; } = null!;

        [Name("orderDate")]
        [TypeConverter(typeof(NullableDateTimeConverter))]
        public DateTime? OrderDate { get; set; } = null!;

        [Name("requiredDate")]
        [TypeConverter(typeof(NullableDateTimeConverter))]
        public DateTime? RequiredDate { get; set; } = null!;

        [Name("shippedDate")]
        [TypeConverter(typeof(NullableDateTimeConverter))]
        public DateTime? ShippedDate { get; set; } = null!;

        [Name("shipVia")]
        public string ShipperId { get; set; } = null!;

        [Name("freight")]
        public decimal? Freight { get; set; } = null!;

        [Name("shipName")]
        public string ShipName { get; set; } = null!;

        [Name("shipAddress")]
        public string ShipAddress { get; set; } = null!;

        [Name("shipCity")]
        public string ShipCity { get; set; } = null!;

        [Name("shipRegion")]
        public string ShipRegion { get; set; } = null!;

        [Name("shipPostalCode")]
        public string ShipPostalCode { get; set; } = null!;

        [Name("shipCountry")]
        public string ShipCountry { get; set; } = null!;    
    }
}
