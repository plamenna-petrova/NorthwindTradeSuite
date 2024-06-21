using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Persistence;
using NorthwindTradeSuite.Persistence.Seeding;

namespace NorthwindTradeSuite.Infrastructure.Extensions
{
    public static class DatabaseSeedingExtension
    {
        public static async Task SeedDatabaseAsync<T>(this IApplicationBuilder applicationBuilder, ILogger<T> logger)
        {
            using var applicationServicesScope = applicationBuilder.ApplicationServices.CreateScope();
            using var applicationDbContext = applicationServicesScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();          
            using IDbContextTransaction dbContextTransaction = applicationDbContext.Database.BeginTransaction();

            logger.LogInformation("Db context transaction began");

            try
            {
                await ApplicationDbContextSeeder.ExecuteDatabaseSeeders(applicationServicesScope.ServiceProvider);
                logger.LogInformation("Database seeding finished");
                await dbContextTransaction.CommitAsync();
                logger.LogInformation("Db context transaction committed");
            }
            catch (Exception exception)
            {
                logger.LogError($"Database seeding error: {exception.Message}{(exception.InnerException != null ? $"\n{exception.InnerException.Message}" : string.Empty)}");
                await dbContextTransaction.RollbackAsync();
                logger.LogInformation("Db context rollback");
            }
        }
    }
}
