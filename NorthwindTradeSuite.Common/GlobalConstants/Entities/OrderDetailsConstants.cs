using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class OrderDetailsConstants
    {
        public const string ORDER_DETAILS_TABLE_NAME = "OrderDetails";

        public const string ORDER_ID_COLUMN = "OrderID";

        public const string PRODUCT_ID_COLUMN = "ProductID";

        public const string ORDER_DETAILS_UNIT_PRICE_COLUMN = "UnitPrice";

        public const string ORDER_DETAILS_QUANTITY_COLUMN = "Quantity";

        public const string ORDER_DETAILS_DISCOUNT_COLUMN = "Discount";

        public const string ORDERS_CONSTAINT_NAME = "FK_Order_Details_Orders";

        public const string PRODUCTS_CONSTAINT_NAME = "FK_Order_Details_Products";
    }
}
