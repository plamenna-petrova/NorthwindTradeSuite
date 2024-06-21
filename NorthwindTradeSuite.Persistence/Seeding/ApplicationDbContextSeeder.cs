using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Persistence.Seeders.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.CSVFileNamesConstants;

namespace NorthwindTradeSuite.Persistence.Seeding
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task ExecuteDatabaseSeeders(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            using var seedersServiceScope = serviceProvider.CreateScope();

            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
            ILogger logger = loggerFactory.CreateLogger(typeof(ApplicationDbContext));

            var seeders = new List<ISeeder>
            {
                new CategoriesSeeder(serviceProvider, logger, CATEGORIES_CSV_FILE_NAME),
                new CustomersSeeder(serviceProvider, logger, CUSTOMERS_CSV_FILE_NAME),
                new EmployeesSeeder(serviceProvider, logger, EMPLOYEES_CSV_FILE_NAME),
                new RegionsSeeder(serviceProvider, logger, REGIONS_CSV_FILE_NAME),
                new TerritoriesSeeder(serviceProvider, logger, TERRITORIES_CSV_FILE_NAME)
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
