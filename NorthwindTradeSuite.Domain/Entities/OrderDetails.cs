using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class OrderDetails : BaseEntity<string>
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
