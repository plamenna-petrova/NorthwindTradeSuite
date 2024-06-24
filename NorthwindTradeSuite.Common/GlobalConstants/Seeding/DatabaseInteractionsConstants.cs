using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Seeding
{
    public class DatabaseInteractionsConstants
    {
        public const string CATEGORIES_RECORDS = "categories";

        public const string CUSTOMERS_RECORDS = "customers";

        public const string EMPLOYEES_RECORDS = "employees";

        public const string REGIONS_RECORDS = "regions";

        public const string TERRITORIES_RECORDS = "territories";

        public const string EMPLOYEE_TERRITORIES_RECORDS = "employee territories";

        public const string SHIPPERS_RECORDS = "shippers";

        public const string SUPPLIERS_RECORDS = "suppliers";

        public const string PRODUCTS_RECORDS = "products";

        public const string ORDERS_RECORDS = "orders";

        public const string ORDER_DETAILS_RECORDS = "order details";

        public const string FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE = "Found {0} in the database. No need to trigger seeding.";

        public const string SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE = "Sample {0} successfully seeded in the database";
    }
}
