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

            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
            ILogger logger = loggerFactory.CreateLogger(typeof(ApplicationDbContext));

            var seeders = new List<ISeeder>
            {
                new CategoriesSeeder(serviceProvider, logger, CATEGORIES_CSV_FILE_NAME)
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }

        public static bool ShouldSeedersBeExecuted(IServiceProvider serviceProvider)
        {
            var applicationDbContext = serviceProvider.GetService<ApplicationDbContext>()!;
            return !applicationDbContext.Roles.Any();
        }
    }
}
