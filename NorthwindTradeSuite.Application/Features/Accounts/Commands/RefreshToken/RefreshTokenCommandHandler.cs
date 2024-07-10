using Microsoft.AspNetCore.Identity;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.DTOs.Responses.Accounts;
using NorthwindTradeSuite.Services.Identity.Tokens;

namespace NorthwindTradeSuite.Application.Features.Accounts.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RequestResult<RefreshTokenResponseDTO>>
    {
        private readonly IJWTService _jwtService;

        private readonly UserManager<ApplicationUser> _userManager;

        public RefreshTokenCommandHandler(IJWTService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }
        
        public async Task<RequestResult<RefreshTokenResponseDTO>> Handle(RefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken)
        {
            var claimsPrincipalFromExpiredAccessToken = _jwtService.GetClaimsPrincipalFromExpiredToken(refreshTokenCommand.RefreshTokenRequestDTO.AccessToken);
            var userName = claimsPrincipalFromExpiredAccessToken.Identity!.Name;

            var userToFindByName = await _userManager.FindByNameAsync(userName);

            if (userToFindByName == null || userToFindByName.RefreshToken != refreshTokenCommand.RefreshTokenRequestDTO.RefreshToken || userToFindByName.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return RequestResult<RefreshTokenResponseDTO>.Failure("Invalid token");
            }

            var newAccessToken = await _jwtService.GenerateAccessTokenAsync(userToFindByName);
            var newRefreshToken = await _jwtService.GenerateRefreshTokenAsync(userToFindByName);

            return RequestResult<RefreshTokenResponseDTO>.Success(new RefreshTokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
