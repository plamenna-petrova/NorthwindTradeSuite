using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities.OwnedEntities
{
    public class LocationData
    {
        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Region { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
