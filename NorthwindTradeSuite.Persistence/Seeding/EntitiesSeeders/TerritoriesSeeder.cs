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
    public class TerritoriesSeeder : BaseSeeder
    {
        public TerritoriesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {
                
        }

        public override async Task SeedAsync()
        {
            var territoryDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Territory>>();

            if (territoryDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, TERRITORIES_RECORDS);
            }
            else
            {
                IDatasetSeedingTarget<SeedTerritoryDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedTerritoryDTO>(DatasetFileName);

                var territoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);
                var mappedTerritoriesForSeeding = territoriesForSeeding.To<Territory>().ToArray();

                foreach (var mappedTerritoryForSeeding in mappedTerritoriesForSeeding)
                {
                    mappedTerritoryForSeeding.CreatedBy = ApplicationUserConstants.SEEDED_ADMINISTRATOR_ID;
                }

                await territoryDeletableRepository.AddRangeAsync(mappedTerritoriesForSeeding);
                await territoryDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, TERRITORIES_RECORDS);
            }
        }
    }
}
