using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class EmployeeTerritoryConstants
    {
        public const string EMPLOYEE_ID_COLUMN = "EmployeeID";

        public const string TERRITORY_ID_COLUMN = "TerritoryID";

        public const string EMPLOYEES_CONSTRAINT_NAME = "FK_EmployeeTerritories_Employees";

        public const string TERRITORIES_CONSTRAINT_NAME = "FK_EmployeeTerritories_Territories";
    }
}
