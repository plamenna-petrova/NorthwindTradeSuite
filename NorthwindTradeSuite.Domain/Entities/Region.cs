using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Region : BaseDeletableEntity
    {
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        public string Description { get; set; } = null!;

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
