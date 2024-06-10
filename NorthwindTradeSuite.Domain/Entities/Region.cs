using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Region : BaseEntity<string>
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        public string Description { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
