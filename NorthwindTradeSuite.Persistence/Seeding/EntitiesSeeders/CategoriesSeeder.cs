using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Common.GlobalConstants.Seeding;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders
{
    public class SeedCategoryDTO
    {
        [Name("categoryID")]
        public string Id { get; set; }

        [Name("categoryName")]
        public string Name { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Name("picture")]
        public byte[] Picture { get; set; } 
    }

    public class CategoriesSeeder : BaseSeeder
    {
        public CategoriesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName) 
            : base(serviceProvider, logger, datasetFileName)  
        {

        }
        
        public override async Task SeedAsync()
        {
            var deletableEntityRepositoryForCategory = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Category>>();

            IDatasetSeedingTarget<SeedCategoryDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedCategoryDTO>(DatasetFileName);

            var categoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

            var mappedCategoriesForSeeding = categoriesForSeeding
                .Select(cfs => new Category
                {
                    Id = cfs.Id,
                    Name = cfs.Name,
                    Description = cfs.Description,
                    Picture = cfs.Picture
                })
                .ToList();

            foreach (var categoryForSeeding in mappedCategoriesForSeeding)
            {
                await deletableEntityRepositoryForCategory.AddAsync(categoryForSeeding);
            }

            await deletableEntityRepositoryForCategory.SaveChangesAsync();
        }
    }
}
