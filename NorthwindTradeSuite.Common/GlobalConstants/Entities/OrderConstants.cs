using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class OrderConstants
    {
        public const string ORDER_ID_COLUMN = "OrderID";

        public const string ORDER_CUSTOMER_ID_COLUMN = "CustomerID";

        public const string ORDER_EMPLOYEE_ID_COLUMN = "EmployeeID";

        public const string ORDER_DATE_COLUMN = "OrderDate";

        public const string ORDER_REQUIRED_DATE_COLUMN = "RequiredDate";

        public const string ORDER_SHIPPED_DATE_COLUMN = "ShippedDate";

        public const string ORDER_SHIPPED_VIA_COLUMN = "ShippedVia";

        public const string ORDER_FREIGHT_COLUMN = "Freight";

        public const string ORDER_SHIP_NAME_COLUMN = "ShipName";

        public const string ORDER_SHIP_ADDRESS_COLUMN = "ShipAddress";

        public const string ORDER_SHIP_CITY_COLUMN = "ShipCity";

        public const string ORDER_SHIP_REGION_COLUMN = "ShipRegion";

        public const string ORDER_SHIP_POSTAL_CODE_COLUMN = "ShipPostalCode";

        public const string ORDER_SHIP_COUNTRY_COLUMN = "ShipCountry";

        public const string ORDER_CUSTOMER_CONSTAINT_NAME = "FK_Orders_Customer";

        public const string ORDER_EMPLOYEE_CONSTAINT_NAME = "FK_Orders_Employee";

        public const string ORDER_SHIPPER_CONSTAINT_NAME = "FK_Orders_Shipper";

        public const int ORDER_SHIP_NAME_MAX_LENGTH = 40;

        public const int ORDER_SHIP_ADDRESS_MAX_LENGTH = 60;

        public const int ORDER_SHIP_CITY_MAX_LENGTH = 15;

        public const int ORDER_SHIP_REGION_MAX_LENGTH = 15;

        public const int ORDER_SHIP_POSTAL_CODE_MAX_LENGTH = 10;

        public const int ORDER_SHIP_COUNTRY_MAX_LENGTH = 15;
    }
}
