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
    public class CategoriesSeeder : BaseSeeder
    {
        public CategoriesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName) 
            : base(serviceProvider, logger, datasetFileName)  
        {

        }
        
        public override async Task SeedAsync()
        {
            var categoryDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Category>>();

            if (categoryDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CATEGORIES_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedCategoryDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedCategoryDTO>(DatasetFileName);

                var categoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedCategoriesForSeeding = categoriesForSeeding.To<Category>().ToArray();

                await categoryDeletableRepository.AddRangeAsync(mappedCategoriesForSeeding);
                await categoryDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CATEGORIES_RECORDS));
            }
        }
    }
}
