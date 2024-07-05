using Newtonsoft.Json;
using NorthwindTradeSuite.Common.GlobalConstants.Identity;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.Mapping.Contracts;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedUserDTO : IMapTo<ApplicationUser>
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        [JsonProperty("userName")]
        public string UserName { get; set; } = null!;

        [Required]
        [RegularExpression(ApplicationUserConstants.EMAIL_REGEX)]
        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(ApplicationUserConstants.COMPLEX_PASSWORD_REGEX)]
        [JsonProperty("password")]
        public string Password { get; set; } = null!;

        [Required]
        [JsonProperty("roles")]
        public List<string> Roles { get; set; } = null!;

        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
