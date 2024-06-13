using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities.OwnedEntities
{
    public class ProfessionalData
    {
        public string CompanyName { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactTitle { get; set; } = null!;
    }
}
