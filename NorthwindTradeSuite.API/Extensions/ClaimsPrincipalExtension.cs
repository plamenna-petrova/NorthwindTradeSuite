using System.Security.Claims;

namespace NorthwindTradeSuite.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string? GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
