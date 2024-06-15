using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Persistence.Seeders.Contracts
{
    public interface ISeeder
    {
        Task SeedAsync();
    }
}
