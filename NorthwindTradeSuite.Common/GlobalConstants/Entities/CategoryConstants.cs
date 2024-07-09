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

        public const string CATEGORY_NAME_LENGTH_ERROR_MESSAGE = "The category name must be between 4 and 50 characters long";

        public const string REQUIRED_CATEGORY_DESCRIPTION_ERROR_MESSAGE = "The category description is required";

        public const string CATEGORY_DESCRIPTION_LENGTH_ERROR_MESSAGE = "The category description must be between 10 and 100 characters long";

        public const string REQUIRED_CATEGORY_PICTURE_ERROR_MESSAGE = "The category picture is required";

        public const string REQUIRED_CATEGORY_CREATED_BY_ERROR_MESSAGE = "The category created by user id is required";

        // API
        public const string CATEGORIES_NAME = "categories";

        public const string SINGLE_CATEGORY_NAME = "category";

        public const string CATEGORY_BY_ID_ROUTE_NAME = "CategoryById";

        public const string CATEGORY_DETAILS_ROUTE_NAME = "CategoryDetails";

        public const string UPDATED_CATEGORY_SUCCESS_MESSAGE = "Successfully updated category";

        public const string DELETED_CATEGORY_SUCCESS_MESSAGE = "Successfully deleted category";

        public const string CONFIRMED_CATEGORY_DELETION_SUCCESS_MESSAGE = "Successfully confirmed category deletion";

        public const string RESTORED_CATEGORY_SUCCESS_MESSAGE = "Successfully restored category";
    }
}
