using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Product : BaseDeletableEntity
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string Name { get; set; } = null!;

        public string SupplierId { get; set; } = null!;

        public virtual Supplier Supplier { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;

        public string QuantityPerUnit { get; set; } = null!;

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        
        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
