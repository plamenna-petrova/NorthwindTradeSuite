using CsvHelper.Configuration.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedProductDTO : IMapTo<Product>
    {
        [Name("productID")]
        public string Id { get; set; } = null!;

        [Name("productName")]
        public string Name { get; set; } = null!;

        [Name("supplierID")]
        public string SupplierId { get; set; } = null!;

        [Name("categoryID")]
        public string CategoryId { get; set; } = null!;

        [Name("quantityPerUnit")]
        public string QuantityPerUnit { get; set; } = null!;

        [Name("unitPrice")]
        public decimal? UnitPrice { get; set; } = null!;

        [Name("unitsInStock")]
        public short? UnitsInStock { get; set; } = null!;

        [Name("unitsOnOrder")]
        public short? UnitsOnOrder { get; set; } = null!;

        [Name("reorderLevel")]
        public short? ReorderLevel { get; set; } = null!;

        [Name("discontinued")]
        public bool Discontinued { get; set; }
    }
}
