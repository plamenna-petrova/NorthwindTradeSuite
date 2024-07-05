using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DatabaseInteractionsConstants;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders
{
    public class OrdersSeeder : BaseSeeder
    {
        public OrdersSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var deletableOrderRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Order>>();

            if (deletableOrderRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, ORDERS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedOrderDTO> databaseSeedingTarget = new DatasetSeedingAdapter<SeedOrderDTO>(DatasetFileName);

                var ordersForSeeding = databaseSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedOrdersForSeeding = ordersForSeeding.To<Order>().ToArray();    

                await deletableOrderRepository.AddRangeAsync(mappedOrdersForSeeding);
                await deletableOrderRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, ORDERS_RECORDS));
            }
        }
    }
}
