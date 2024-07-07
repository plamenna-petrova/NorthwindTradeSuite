using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Identity
{
    public class ApplicationUserConstants
    {
        // Database
        public const string REFRESH_TOKEN_COLUMN = "RefreshToken";

        public const string REFRESH_TOKEN_EXPIRY_TIME_COLUMN = "RefreshTokenExpiryTime";

        // Validation
        public const string REQUIRED_USERNAME_ERROR_MESSAGE = "The username is required";

        public const string REQUIRED_EMAIL_ERROR_MESSAGE = "The email is required";

        public const string REQUIRED_VALID_EMAIL_ERROR_MESSAGE = "Valid email is required";

        public const string EMAIL_REGEX = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

        public const int PASSWORD_MINIMUM_LENGTH = 10;

        public const string REQUIRED_PASSWORD_ERROR_MESSAGE = "The password is required";

        public const string PASSWORD_MINIMUM_LENGTH_ERROR_MESSAGE = "The password must be at least 10 characters long.";

        public const string COMPLEX_PASSWORD_REGEX = "^(?=.*[a-z].*[a-z])(?=.*[A-Z].*[A-Z])(?=.*\\d.*\\d)(?=.*[!@#$%^&*()_+].*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{10,}$";

        public const string COMPLEX_PASSWORD_REQUIREMENTS_NOT_MET_ERROR_MESSAGE = "The password must meet complexity requirements.";

        public const string REQUIRED_CONFIRM_PASSWORD_ERROR_MESSAGE = "The confirm password is required";

        public const string CONFIRM_PASSWORD_MISMATCH_ERROR_MESSAGE = "The confirm password must match the password";

        public const string USER_REGISTRATION_SUCCESS_MESSAGE = "User registered successfully";

        public const string REQUIRED_ACCESS_TOKEN_ERROR_MESSAGE = "Access token is required";

        public const string REQUIRED_REFRESH_TOKEN_ERROR_MESSAGE = "Refresh token is required";
    }
}
