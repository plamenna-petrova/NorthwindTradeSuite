using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.Domain.Entities.Identity;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RequestResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RequestResult> Handle(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            var applicationUserToCreate = new ApplicationUser
            {
                UserName = registerCommand.RegisterRequestDTO.UserName,
                Email = registerCommand.RegisterRequestDTO.Email,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            if (_userManager.Users.All(u => u.UserName != applicationUserToCreate.UserName))
            {
                var createApplicationUserResult = await _userManager.CreateAsync(applicationUserToCreate, registerCommand.RegisterRequestDTO.Password);

                if (createApplicationUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUserToCreate, NORMAL_USER);
                    return RequestResult.Success(USER_REGISTRATION_SUCCESS_MESSAGE);
                }

                return RequestResult.Failure(createApplicationUserResult.Errors.Select(ie => ie.Description).ToArray());
            }

            return RequestResult.Failure(new string[] { $"A user with the username: '{applicationUserToCreate.UserName}' already exists" });
        }
    }
}
