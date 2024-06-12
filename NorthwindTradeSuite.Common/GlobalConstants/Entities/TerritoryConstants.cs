using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.GlobalConstants.Entities
{
    public class TerritoryConstants
    {
        public const string TERRITORY_ID_COLUMN = "TerritoryID";

        public const string TERRITORY_DESCRIPTION_COLUMN = "TerritoryDescription";

        public const string TERRITORY_REGION_ID_COLUMN = "RegionID";

        public const string TERRITORY_REGION_CONSTRAINT_NAME = "FK_Territories_Region";

        public const int TERRITORY_DESCRIPTION_MAX_LENGTH = 50;
    }
}
