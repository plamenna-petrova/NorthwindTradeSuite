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
    public class ProductsSeeder : BaseSeeder
    {
        public ProductsSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var deletableProductRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Product>>();

            if (deletableProductRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, PRODUCTS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedProductDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedProductDTO>(DatasetFileName);

                var productsForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();
                var mappedProductsForSeeding = productsForSeeding.To<Product>().ToArray();

                await deletableProductRepository.AddRangeAsync(mappedProductsForSeeding);
                await deletableProductRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, PRODUCTS_RECORDS));
            }
        }
    }
}
