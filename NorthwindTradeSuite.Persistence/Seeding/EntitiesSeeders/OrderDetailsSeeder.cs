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
    public class OrderDetailsSeeder : BaseSeeder
    {
        public OrderDetailsSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var orderDetailsRepository = ServiceProvider.GetRequiredService<IBaseRepository<OrderDetails>>();

            if (orderDetailsRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, ORDER_DETAILS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedOrderDetailsDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedOrderDetailsDTO>(DatasetFileName);

                var orderDetailsForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();
                var mappedOrderDetailsForSeeding = orderDetailsForSeeding.To<OrderDetails>().ToArray();

                await orderDetailsRepository.AddRangeAsync(mappedOrderDetailsForSeeding);
                await orderDetailsRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, ORDER_DETAILS_RECORDS));
            }
        }
    }
}
