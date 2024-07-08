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
    public class RegionsSeeder : BaseSeeder
    {
        public RegionsSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {
                
        }

        public override async Task SeedAsync()
        {
            var regionDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Region>>();

            if (regionDeletableRepository.GetAll(asNoTracking: true).Any()) 
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, REGIONS_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedRegionDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedRegionDTO>(DatasetFileName);

                var regionsForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedRegionsForSeeding = regionsForSeeding.To<Region>().ToArray();

                foreach (var mappedRegionForSeeding in mappedRegionsForSeeding)
                {
                    mappedRegionForSeeding.CreatedBy = ApplicationUserConstants.SEEDED_ADMINISTRATOR_ID;
                }

                await regionDeletableRepository.AddRangeAsync(mappedRegionsForSeeding);
                await regionDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, REGIONS_RECORDS));
            }
        }
    }
}
