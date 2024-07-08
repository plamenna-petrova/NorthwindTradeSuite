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

                var suppliersForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedSuppliersForSeeding = suppliersForSeeding.To<Supplier>().ToArray();

                foreach (var mappedSupplierForSeeding in mappedSuppliersForSeeding)
                {
                    mappedSupplierForSeeding.CreatedBy = ApplicationUserConstants.SEEDED_ADMINISTRATOR_ID;
                }

                await supplierDeletableRepository.AddRangeAsync(mappedSuppliersForSeeding);
                await supplierDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, SUPPLIERS_RECORDS));
            }
        }
    }
}
