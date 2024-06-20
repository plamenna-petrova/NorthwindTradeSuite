using CsvHelper.Configuration;
using NorthwindTradeSuite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders.ClassMappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Map(c => c.Id).Name("categoryID");
            Map(c => c.Name).Name("categoryName");
            Map(c => c.Description).Name("description");
            Map(c => c.Picture).Name("picture");
        }
    }
}
