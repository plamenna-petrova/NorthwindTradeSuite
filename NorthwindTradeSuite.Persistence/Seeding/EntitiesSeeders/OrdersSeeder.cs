using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;
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

                var ordersForSeeding = databaseSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedOrdersForSeeding = ordersForSeeding
                    .Select(o => new Order
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        EmployeeId = o.EmployeeId,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        ShipperId = o.ShipperId,
                        Freight = o.Freight,
                        ShipName = o.ShipName,
                        LocationData = new LocationData
                        {
                            Address = o.ShipAddress,
                            City = o.ShipCity,
                            Region = o.ShipRegion,
                            PostalCode = o.ShipPostalCode,
                            Country = o.ShipCountry
                        }
                    })
                    .ToArray();

                await deletableOrderRepository.AddRangeAsync(mappedOrdersForSeeding);
                await deletableOrderRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, ORDERS_RECORDS));
            }
        }
    }
}
