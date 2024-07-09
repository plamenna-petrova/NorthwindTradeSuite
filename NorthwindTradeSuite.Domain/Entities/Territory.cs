using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Territory : BaseDeletableEntity
    {
        public Territory()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
        }

        public string Description { get; set; } = null!;

        public string RegionId { get; set; } = null!;

        public virtual Region Region { get; set; } = null!;

        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
