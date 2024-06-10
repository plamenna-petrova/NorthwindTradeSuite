using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public string ShipperId { get; set; }

        public virtual Shipper Shipper { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
