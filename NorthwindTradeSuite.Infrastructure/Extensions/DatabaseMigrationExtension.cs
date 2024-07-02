using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NorthwindTradeSuite.Infrastructure.Extensions
{
    public static class DatabaseMigrationExtension
    {
        public static async Task MigrateDatabaseAsync<T>(this IApplicationBuilder applicationBuilder, ILogger<T> logger)
        {
            try
            {
                using var applicationServicesScope = applicationBuilder.ApplicationServices.CreateScope();

                using var applicationDbContext = applicationServicesScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                IEnumerable<string> pendingMigrationsEnumerable = await applicationDbContext.Database.GetPendingMigrationsAsync();
                List<string> listOfPendingMigrations = pendingMigrationsEnumerable.ToList();

                if (listOfPendingMigrations.Any())
                {
                    await applicationDbContext.Database.MigrateAsync();
                    logger.LogInformation($"Pending migrations applied to the database");
                }
                else
                {
                    logger.LogInformation($"No pending migrations need to be applied to the database");
                }
            }
            catch (Exception exception)
            {
                logger.LogError($"Database Migration failed: {exception.Message}");
            }
        }
    }
}
