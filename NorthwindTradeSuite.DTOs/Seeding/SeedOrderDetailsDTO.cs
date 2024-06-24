using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedOrderDetailsDTO
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
