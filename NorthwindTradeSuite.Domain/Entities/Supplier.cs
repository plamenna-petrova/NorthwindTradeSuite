using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Supplier : BaseDeletableEntity
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public ProfessionalData ProfessionalData { get; set; } = new();

        public LocationData LocationData { get; set; } = new();

        public PersonalContactData PersonalContactData { get; set; } = new();

        public string HomePage { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
