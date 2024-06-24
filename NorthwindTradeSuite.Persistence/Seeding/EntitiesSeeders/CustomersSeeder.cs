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

                var mappedCustomersForSeeding = customersForSeeding
                    .Select(cust => new Customer
                    {
                        Id = cust.Id,
                        ProfessionalData = new ProfessionalData
                        {
                            CompanyName = cust.SeedProfessionalDataDTO.CompanyName,
                            ContactName = cust.SeedProfessionalDataDTO.ContactName,
                            ContactTitle = cust.SeedProfessionalDataDTO.ContactTitle
                        },
                        LocationData = new LocationData
                        {
                            Address = cust.SeedLocationDataDTO.Address,
                            City = cust.SeedLocationDataDTO.City,
                            Region = cust.SeedLocationDataDTO.Region,
                            PostalCode = cust.SeedLocationDataDTO.PostalCode,
                            Country = cust.SeedLocationDataDTO.Country,
                        },
                        PersonalContactData = new PersonalContactData
                        {
                            Phone = cust.SeedPersonalContactDataDTO.Phone,
                            Fax = cust.SeedPersonalContactDataDTO.Fax
                        }
                    })
                    .ToArray();

                await customerDeletableRepository.AddRangeAsync(mappedCustomersForSeeding);
                await customerDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CUSTOMERS_RECORDS));
            }
        }
    }
}
