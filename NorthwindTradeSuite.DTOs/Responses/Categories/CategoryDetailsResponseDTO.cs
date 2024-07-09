using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Responses.Categories
{
    public class CategoryDetailsResponseDTO : IMapFrom<Category>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; } = null!;
    }
}
