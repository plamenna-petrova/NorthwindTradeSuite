using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class CategoryConstants
    {
        // Database
        public const string CATEGORY_ID_COLUMN = "CategoryID";

        public const string CATEGORY_NAME_COLUMN = "CategoryName";

        public const string CATEGORY_DESCRIPTION_COLUMN = "Description";

        public const string CATEGORY_PICTURE_COLUMN = "Picture";

        public const int CATEGORY_NAME_MAX_LENGTH = 50;

        public const int CATEGORY_DESCRIPTION_MAX_LENGTH = 300;

        // Validation
        public const string REQUIRED_CATEGORY_NAME_ERROR_MESSAGE = "The category name is required";

        public const string REQUIRED_CATEGORY_DESCRIPTION_ERROR_MESSAGE = "The category description is required";

        public const string REQUIRED_CATEGORY_PICTURE_ERROR_MESSAGE = "The category picture is required";

        public const string REQUIRED_CATEGORY_CREATED_BY_ERROR_MESSAGE = "The category created by user id is required";

        // API
        public const string CategoriesName = "categories";

        public const string SingleCategoryName = "category";

        public const string CategoryByIdRouteName = "CategoryById";

        public const string CategoryDetailsRouteName = "CategoryDetails";
    }
}
