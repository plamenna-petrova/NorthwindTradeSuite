using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Order : BaseDeletableEntity<string>
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string CustomerId { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;

        public string EmployeeId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string ShipperId { get; set; } = null!;

        public virtual Shipper Shipper { get; set; } = null!;

        public decimal? Freight { get; set; }

        public string ShipName { get; set; } = null!;

        public LocationData LocationData { get; set; } = new();

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
