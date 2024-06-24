using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.OwnedEntities;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DatabaseInteractionsConstants;

namespace NorthwindTradeSuite.Persistence.Seeding.EntitiesSeeders
{
    public class EmployeesSeeder : BaseSeeder
    {
        public EmployeesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var employeeDeletableRepository = ServiceProvider.GetRequiredService<IDeletableEntityRepository<Employee>>();

            if (employeeDeletableRepository.GetAll(asNoTracking: true).Any())
            {
                Logger.LogInformation(string.Format(FOUND_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, EMPLOYEES_RECORDS));
            }
            else
            {
                IDatasetSeedingTarget<SeedEmployeeDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedEmployeeDTO>(DatasetFileName);

                var employeesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding();

                var mappedEmployeesForSeeding = employeesForSeeding
                    .Select(emp => new Employee
                    {
                        Id = emp.Id,
                        LastName = emp.LastName,
                        FirstName = emp.FirstName,
                        Title = emp.Title,
                        TitleOfCourtesy = emp.TitleOfCourtesy,
                        BirthDate = emp.BirthDate,
                        HireDate = emp.HireDate,
                        LocationData = new LocationData
                        {
                            Address = emp.SeedLocationDTO.Address,
                            City = emp.SeedLocationDTO.City,
                            Region = emp.SeedLocationDTO.Region,
                            PostalCode = emp.SeedLocationDTO.PostalCode,
                            Country = emp.SeedLocationDTO.Country
                        },
                        HomePhone = emp.HomePhone,
                        Extension = emp.Extension,
                        Photo = emp.Photo,
                        Notes = emp.Notes,
                        PhotoPath = emp.PhotoPath
                    })
                    .ToArray();

                await employeeDeletableRepository.AddRangeAsync(mappedEmployeesForSeeding);
                await employeeDeletableRepository.SaveChangesAsync();

                foreach (var mappedEmployeeForSeeding in mappedEmployeesForSeeding)
                {
                    var employeeForSeedingReportsTo = employeesForSeeding.SingleOrDefault(emp => emp.Id == mappedEmployeeForSeeding.Id)!.ReportsTo!;
                    var employeeManager = await employeeDeletableRepository.GetByIdAsync(employeeForSeedingReportsTo);
                    mappedEmployeeForSeeding.Manager = employeeManager!;
                }

                employeeDeletableRepository.UpdateRange(mappedEmployeesForSeeding);
                await employeeDeletableRepository.SaveChangesAsync();

                Logger.LogInformation(string.Format(SUCCESSFULLY_SEEDED_RECORDS_IN_THE_DATABASE_INFORMATION_MESSAGE, EMPLOYEES_RECORDS));
            }
        }
    }
}
