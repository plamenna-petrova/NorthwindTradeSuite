using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders
{
    public class SeedCategoryDTO
    {
        [Name("categoryID")]
        public string Id { get; set; } = null!;

        [Name("categoryName")]
        public string Name { get; set; } = null!;

        [Name("description")]
        public string Description { get; set; } = null!;

        [Name("picture")]
        public byte[] Picture { get; set; } = null!;
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

            if (!deletableEntityRepositoryForCategory.GetAll(asNoTracking: true).Any())
            {
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
                    .ToArray();

                await deletableEntityRepositoryForCategory.AddRangeAsync(mappedCategoriesForSeeding);
                await deletableEntityRepositoryForCategory.SaveChangesAsync();
            }
        }
    }
}
