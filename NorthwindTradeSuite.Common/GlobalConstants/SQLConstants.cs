using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants
{
    public class SQLConstants
    {
        public const string GET_DATE_FUNCTION = "GETDATE()";

        public const string YEAR_FUNCTION_TEMPLATE = "YEAR({0})";

        public const string CHECK_CONSTRAINT_TEMPLATE = "CK_{0}";

        public const string UNIQUE_INDEX_TEMPLATE = "IX_{0}";

        public const string DECIMAL_DATA_TYPE_TEMPLATE = "DECIMAL({0},{1})";

        public const string NTEXT_COLUMN_TYPE = "ntext";

        public const string IMAGE_COLUMN_TYPE = "image";

        public const string DATETIME_COLUMN_TYPE = "datetime";

        public const string MONEY_COLUMN_TYPE = "money";

        public const string SQL_ZERO_DEFAULT_VALUE = "((0))";

        public const string SQL_ONE_DEFAULT_VALUE = "((1))";
    }
}
