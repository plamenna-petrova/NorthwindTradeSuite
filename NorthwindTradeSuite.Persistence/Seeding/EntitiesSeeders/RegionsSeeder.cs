using Microsoft.EntityFrameworkCore;
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
    public class RegionsSeeder : BaseSeeder
    {
        public RegionsSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {
                
        }

        public override async Task SeedAsync()
        {
            var deletableEntityRepositoryForRegion = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Region>>();

            if (deletableEntityRepositoryForRegion.GetAll(asNoTracking: true).Any()) 
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, REGIONS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedRegionDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedRegionDTO>(DatasetFileName);

                var regionsForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedRegionsForSeeding = regionsForSeeding
                    .Select(r => new Region
                    {
                        Id = r.Id,
                        Description = r.Description,
                    })
                    .ToArray();

                await deletableEntityRepositoryForRegion.AddRangeAsync(mappedRegionsForSeeding);
                await deletableEntityRepositoryForRegion.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, REGIONS_RECORDS));
            }
        }
    }
}
