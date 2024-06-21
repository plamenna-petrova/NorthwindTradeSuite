using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.DTOs.Seeding;
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
            var deletableEntityRepositoryForCategory = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Category>>();

            if (deletableEntityRepositoryForCategory.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CATEGORIES_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedCategoryDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedCategoryDTO>(DatasetFileName);

                var categoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedCategoriesForSeeding = categoriesForSeeding
                    .Select(cat => new Category
                    {
                        Id = cat.Id,
                        Name = cat.Name,
                        Description = cat.Description,
                        Picture = cat.Picture
                    })
                    .ToArray();

                await deletableEntityRepositoryForCategory.AddRangeAsync(mappedCategoriesForSeeding);
                await deletableEntityRepositoryForCategory.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, CATEGORIES_RECORDS));
            }
        }
    }
}
