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
    public class SuppliersSeeder : BaseSeeder
    {
        public SuppliersSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var supplierDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Supplier>>();

            if (supplierDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, SUPPLIERS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedSupplierDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedSupplierDTO>(DatasetFileName);

                var suppliersForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedSuppliersForSeeding = suppliersForSeeding
                    .Select(sup => new Supplier
                    {
                        Id = sup.Id,
                        ProfessionalData = new ProfessionalData
                        {
                            CompanyName = sup.SeedProfessionalDataDTO.CompanyName,
                            ContactName = sup.SeedProfessionalDataDTO.ContactName,
                            ContactTitle = sup.SeedProfessionalDataDTO.ContactTitle
                        },
                        LocationData = new LocationData
                        {
                            Address = sup.SeedLocationDataDTO.Address,
                            City = sup.SeedLocationDataDTO.City,
                            Region = sup.SeedLocationDataDTO.Region,
                            PostalCode = sup.SeedLocationDataDTO.PostalCode,
                            Country = sup.SeedLocationDataDTO.Country
                        },
                        PersonalContactData = new PersonalContactData
                        {
                            Phone = sup.SeedPersonalContactDataDTO.Phone,
                            Fax = sup.SeedPersonalContactDataDTO.Fax
                        }
                    })
                    .ToArray();

                await supplierDeletableRepository.AddRangeAsync(mappedSuppliersForSeeding);
                await supplierDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, SUPPLIERS_RECORDS));
            }
        }
    }
}
