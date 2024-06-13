using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities.OwnedEntities
{
    public class PersonalContactData
    {
        public string Phone { get; set; } = null!;

        public string Fax { get; set; } = null!;
    }
}
