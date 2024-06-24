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
    public class EmployeeTerritoriesSeeder : BaseSeeder
    {
        public EmployeeTerritoriesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var employeeTerritoryRepository = ServiceProvider.GetRequiredService<IBaseRepository<EmployeeTerritory>>();

            if (employeeTerritoryRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, EMPLOYEE_TERRITORIES_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedEmployeeTerritoryDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedEmployeeTerritoryDTO>(DatasetFileName);

                var employeeTerritoriesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedEmployeeTerritoriesForSeeding = employeeTerritoriesForSeeding
                    .Select(et => new EmployeeTerritory
                    {
                        EmployeeId = et.EmployeeId,
                        TerritoryId = et.TerritoryId
                    })
                    .ToArray();

                await employeeTerritoryRepository.AddRangeAsync(mappedEmployeeTerritoriesForSeeding);
                await employeeTerritoryRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, EMPLOYEE_TERRITORIES_RECORDS));
            }
        }
    }
}
