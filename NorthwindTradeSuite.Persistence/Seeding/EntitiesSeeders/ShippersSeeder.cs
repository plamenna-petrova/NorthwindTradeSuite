using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Common.GlobalConstants.Identity;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.Mapping.AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DatabaseInteractionsConstants;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders
{
    public class ShippersSeeder : BaseSeeder
    {
        public ShippersSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var shipperDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Shipper>>();

            if (shipperDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, SHIPPERS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedShipperDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedShipperDTO>(DatasetFileName);

                var shippersForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedShippersForSeeding = shippersForSeeding.To<Shipper>().ToArray();

                foreach (var mappedShipperForSeeding in mappedShippersForSeeding)
                {
                    mappedShipperForSeeding.CreatedBy = ApplicationUserConstants.SEEDED_ADMINISTRATOR_ID;
                }

                await shipperDeletableRepository.AddRangeAsync(mappedShippersForSeeding);
                await shipperDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, SHIPPERS_RECORDS));
            }
        }
    }
}
