using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;

namespace NorthwindTradeSuite.Persistence.Seeding.IdentitySeeders
{
    public class RolesSeeder : BaseSeeder
    {
        public RolesSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName) 
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var roleManager = ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            IDatasetSeedingTarget<SeedRoleDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedRoleDTO>(DatasetFileName);

            var rolesForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);

            foreach (var roleForSeeding in rolesForSeeding)
            {
                if (!await roleManager.RoleExistsAsync(roleForSeeding.Name))
                {
                    var applicationRoleToCreate = new ApplicationRole
                    {
                        Id = roleForSeeding.Id[..8],
                        Name = roleForSeeding.Name,
                        CreatedAt = roleForSeeding.CreatedAt
                    };

                    var createRoleIdentityResult = await roleManager.CreateAsync(applicationRoleToCreate);

                    if (!createRoleIdentityResult.Succeeded)
                    {
                        var createRoleIdentityResultErrosDescriptions = string.Join(", ", createRoleIdentityResult.Errors.Select(ie => ie.Description));
                        Logger.LogError($"Failed to create role {roleForSeeding.Name}, Errors:{createRoleIdentityResultErrosDescriptions}");
                    }
                }
            }
        }
    }
}
