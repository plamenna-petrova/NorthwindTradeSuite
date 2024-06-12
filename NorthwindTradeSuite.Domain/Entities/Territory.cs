using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Territory : BaseEntity<string>
    {
        public Territory()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
        }

        public string Description { get; set; }

        public string RegionId { get; set; }

        public virtual Region Region { get; set; }  

        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
