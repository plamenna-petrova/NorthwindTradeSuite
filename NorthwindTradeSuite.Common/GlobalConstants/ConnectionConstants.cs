using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants
{
    public class ConnectionConstants
    {
        public const string DATABASE_CONNECTION_STRING = @"C:\Users\Plamenna Petrova\AppData\Roaming\Microsoft\UserSecrets\75c217db-d7af-42bf-b0a3-6a83dd3aa27a";

        public const string SECRETS_JSON_FILE_NAME = "secrets.json";

        public const string SECRETS_JSON_CONNECTION_STRING_SECTION = "ConnectionStrings:DefaultConnection";
    }
}
