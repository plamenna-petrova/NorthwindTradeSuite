using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.DTOs.Seeding;
using NorthwindTradeSuite.Persistence.Seeding.Abstraction;
using NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter;

namespace NorthwindTradeSuite.Persistence.Seeding.IdentitySeeders
{
    public class UsersSeeder : BaseSeeder
    {
        public UsersSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName) 
            : base(serviceProvider, logger, datasetFileName)
        {

        }

        public override async Task SeedAsync()
        {
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            IDatasetSeedingTarget<SeedUserDTO> datasetSeedingTarget = new DatasetSeedingAdapter<SeedUserDTO>(DatasetFileName);

            var usersForSeeding = datasetSeedingTarget.RetrieveDatasetObjectsForSeeding(Logger);

            foreach (var userForSeeding in usersForSeeding)
            {
                if (userManager.Users.All(u => u.UserName != userForSeeding.UserName))
                {
                    var applicationUserToCreate = new ApplicationUser
                    {
                        Id = userForSeeding.Id[..8],
                        UserName =userForSeeding.UserName,
                        Email = userForSeeding.Email,
                        CreatedAt = userForSeeding.CreatedAt,
                        EmailConfirmed = true,
                    };

                    var createUserIdentityResult = await userManager.CreateAsync(applicationUserToCreate, userForSeeding.Password);

                    if (createUserIdentityResult.Succeeded)
                    {
                        foreach (var userRole in userForSeeding.Roles)
                        {
                            bool doesUserRoleExist = await roleManager.RoleExistsAsync(userRole);

                            if (doesUserRoleExist)
                            {
                                await userManager.AddToRoleAsync(applicationUserToCreate, userRole);
                            }
                        }
                    }
                    else
                    {
                        var createUserIdentityResultErrorsDescriptions = string.Join(", ", createUserIdentityResult.Errors.Select(ie => ie.Description));
                        Logger.LogError($"Failed to create user: {applicationUserToCreate.UserName}. Errors: {createUserIdentityResultErrorsDescriptions}");
                    }
                }
                else
                {
                    Logger.LogError($"A user with the username '{userForSeeding.UserName}' already exists");
                }
            }
        }
    }
}
