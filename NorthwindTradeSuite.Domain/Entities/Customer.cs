using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Customer : BaseEntity<string>
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public ProfessionalData ProfessionalData { get; set; } = new();

        public LocationData LocationData { get; set; } = new();

        public PersonalContactData PersonalContactData { get; set; } = new();

        public virtual ICollection<Order> Orders { get; set; }
    }
}
