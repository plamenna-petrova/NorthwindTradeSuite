using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string Name { get; set; }

        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        
        public short? UnitsInOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
