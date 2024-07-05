using Newtonsoft.Json;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.Mapping.Contracts;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTradeSuite.DTOs.Seeding
{
    public class SeedRoleDTO : IMapTo<ApplicationRole>
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
