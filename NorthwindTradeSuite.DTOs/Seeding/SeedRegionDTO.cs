using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedRegionDTO
    {
        [Name("regionID")]
        public string Id { get; set; } = null!;

        [Name("regionDescription")]
        public string Description { get; set; } = null!;
    }
}
