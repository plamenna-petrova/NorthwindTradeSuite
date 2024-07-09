using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Shipper : BaseDeletableEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public string CompanyName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
