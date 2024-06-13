using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class ProductConstants
    {
        public const string PRODUCT_ID_COLUMN = "ProductID";

        public const string PRODUCT_NAME_COLUMN = "ProductName";

        public const string PRODUCT_SUPPLIER_ID_COLUMN = "SupplierID";

        public const string PRODUCT_CATEGORY_ID_COLUMN = "CategoryID";

        public const string PRODUCT_QUANTITY_PER_UNIT_COLUMN = "QuantityPerUnit";

        public const string PRODUCT_UNIT_PRICE_COLUMN = "UnitPrice";

        public const string PRODUCT_UNITS_IN_STOCK_COLUMN = "UnitsInStock";

        public const string PRODUCT_UNITS_IN_ORDER_COLUMN = "UnitsInOrder";

        public const string PRODUCT_REORDER_LEVEL_COLUMN = "ReorderLevel";

        public const string PRODUCT_DISCONTINUED_COLUMN = "Discontinued";

        public const string PRODUCT_SUPPLIER_CONSTAINT_NAME = "FK_Products_Supplier";

        public const string PRODUCT_CATEGORY_CONSTRAINT_NAME = "FK_Products_Category";

        public const int PRODUCT_NAME_MAX_LENGTH = 40;

        public const int PRODUCT_QUANTITY_PER_UNIT_MAX_LENGTH = 20;
    }
}
