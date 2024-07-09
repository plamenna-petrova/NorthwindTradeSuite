using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthwindTradeSuite.Services.Identity.Tokens
{
    [TransientService]
    public class JWTService : IJWTService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;

        public JWTService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> GenerateAccessTokenAsync(ApplicationUser applicationUser)
        {
            var applicationUserRoles = await _userManager.GetRolesAsync(applicationUser);

            List<Claim> authClaims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var applicationUserRolesClaims = applicationUserRoles.Select(aur => new Claim(ClaimTypes.Role, aur));

            authClaims.AddRange(applicationUserRolesClaims);

            byte[] jwtKeyBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var symmetricSecurityKey = new SymmetricSecurityKey(jwtKeyBytes);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenString;
        }

        public async Task<string> GenerateRefreshTokenAsync(ApplicationUser applicationUser)
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

            await _userManager.UpdateAsync(applicationUser);

            return refreshToken;
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = issuerSigningKey,
                ValidateLifetime = false
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return claimsPrincipal;
        }
    }
}
