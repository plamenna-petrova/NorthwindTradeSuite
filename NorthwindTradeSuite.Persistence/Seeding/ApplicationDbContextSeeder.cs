using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Persistence.Seeders.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders;
using NorthwindTradeSuite.Persistence.Seeding.IdentitySeeders;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.JSONFileNamesConstants;
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
                new RolesSeeder(serviceProvider, logger, ROLES_JSON_FILE_NAME),
                new UsersSeeder(serviceProvider, logger, USERS_JSON_FILE_NAME),
                new CategoriesSeeder(serviceProvider, logger, CATEGORIES_CSV_FILE_NAME),
                new CustomersSeeder(serviceProvider, logger, CUSTOMERS_CSV_FILE_NAME),
                new EmployeesSeeder(serviceProvider, logger, EMPLOYEES_CSV_FILE_NAME),
                new RegionsSeeder(serviceProvider, logger, REGIONS_CSV_FILE_NAME),
                new TerritoriesSeeder(serviceProvider, logger, TERRITORIES_CSV_FILE_NAME),
                new EmployeeTerritoriesSeeder(serviceProvider, logger, EMPLOYEE_TERRITORIES_CSV_FILE_NAME),
                new ShippersSeeder(serviceProvider, logger, SHIPPERS_CSV_FILE_NAME),
                new SuppliersSeeder(serviceProvider, logger, SUPPLIERS_CSV_FILE_NAME),
                new ProductsSeeder(serviceProvider, logger, PRODUCTS_CSV_FILE_NAME),
                new OrdersSeeder(serviceProvider, logger, ORDERS_CSV_FILE_NAME),
                new OrderDetailsSeeder(serviceProvider, logger, ORDER_DETAILS_CSV_FILE_NAME)
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
