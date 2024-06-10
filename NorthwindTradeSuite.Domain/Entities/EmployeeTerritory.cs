using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class EmployeeTerritory
    {
        public string EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        public string TerritoryID { get; set; }

        public virtual Territory Territory { get; set; }
    }
}
