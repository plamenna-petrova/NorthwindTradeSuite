using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Responses.Categories
{
    public class CategoryConciseInformationResponseDTO : IMapFrom<Category>
    {
        public string Name { get; set; } = null!;
    }
}
