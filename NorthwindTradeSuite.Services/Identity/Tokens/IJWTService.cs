using NorthwindTradeSuite.Domain.Entities.Identity;
using System.Security.Claims;

namespace NorthwindTradeSuite.Services.Identity.Tokens
{
    public interface IJWTService
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser applicationUser);

        Task<string> GenerateRefreshTokenAsync(ApplicationUser applicationUser);

        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}
