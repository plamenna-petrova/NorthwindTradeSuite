using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Territory : BaseEntity
    {
        public Territory()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
        }

        public string Description { get; set; }

        public string RegionID { get; set; }

        public virtual Region Region { get; set; }  

        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
