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
    public class CustomersSeeder : BaseSeeder
    {
        public CustomersSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var customerDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Customer>>();

            if (customerDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CUSTOMERS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedCustomerDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedCustomerDTO>(DatasetFileName);

                var customersForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();
                var mappedCustomersForSeeding = customersForSeeding.To<Customer>().ToArray();

                await customerDeletableRepository.AddRangeAsync(mappedCustomersForSeeding);
                await customerDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CUSTOMERS_RECORDS));
            }
        }
    }
}
