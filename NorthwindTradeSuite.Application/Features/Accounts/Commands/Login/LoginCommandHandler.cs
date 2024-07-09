using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.DTOs.Responses.Accounts;
using NorthwindTradeSuite.Services.Identity.Tokens;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<LoginResponseDTO>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IJWTService _jwtService;

        private readonly UserManager<ApplicationUser> _userManager;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, IJWTService jwtService, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<Result<LoginResponseDTO>> Handle(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(loginCommand.LoginRequestDTO.UserName, loginCommand.LoginRequestDTO.Password, false, lockoutOnFailure: false);

            if (passwordSignInResult.Succeeded)
            {
                var userToLogin = await _userManager.FindByNameAsync(loginCommand.LoginRequestDTO.UserName);

                var accessToken = await _jwtService.GenerateAccessTokenAsync(userToLogin);
                var refreshToken = await _jwtService.GenerateRefreshTokenAsync(userToLogin);

                return Result<LoginResponseDTO>.Success(new LoginResponseDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }

            return Result<LoginResponseDTO>.Failure("Invalid credentials");
        }
    }
}
