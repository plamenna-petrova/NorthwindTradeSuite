using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;
using NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders.ClassMappings;

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

            IDatasetSeedingTarget<Category> datasetSeedingTarget = new DatasetSeedingAdapter<Category, CategoryMap>(DatasetFileName);

            var categoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

            foreach (var categoryForSeeding in categoriesForSeeding)
            {
                await deletableEntityRepositoryForCategory.AddAsync(categoryForSeeding);
            }

            await deletableEntityRepositoryForCategory.SaveChangesAsync();
        }
    }
}
