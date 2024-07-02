using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedOrderDetailsDTO : IMapTo<OrderDetails>
    {
        [Name("orderID")]
        public string OrderId { get; set; } = null!;

        [Name("productID")]
        public string ProductId { get; set; } = null!;

        [Name("unitPrice")]
        public decimal UnitPrice { get; set; }

        [Name("quantity")]
        public short Quantity { get; set; }

        [Name("discount")]
        public float Discount { get; set; }
    }
}
