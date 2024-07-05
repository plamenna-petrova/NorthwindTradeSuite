using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.Domain.Entities.Identity;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationRoleConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Identity.ApplicationUserConstants;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IValidator<RegisterCommand> _validator;

        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IValidator<RegisterCommand> validator)
        {
            _validator = validator;
            _userManager = userManager;
        }

        public async Task<Result> Handle(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(registerCommand);

            var applicationUserToCreate = new ApplicationUser
            {
                UserName = registerCommand.RegisterRequestDTO.UserName,
                Email = registerCommand.RegisterRequestDTO.Email,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var createApplicationUserResult = await _userManager.CreateAsync(applicationUserToCreate, registerCommand.RegisterRequestDTO.Password);

            if (createApplicationUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUserToCreate, NORMAL_USER);
                return Result.Success(USER_REGISTRATION_SUCCESS_MESSAGE);
            }

            return Result.Failure(createApplicationUserResult.Errors.Select(ie => ie.Description).ToArray());
        }
    }
}
