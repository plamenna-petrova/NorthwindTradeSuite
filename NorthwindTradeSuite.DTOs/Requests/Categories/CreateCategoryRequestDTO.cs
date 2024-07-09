using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Mapping.Contracts;

namespace NorthwindTradeSuite.DTOs.Requests.Categories
{
    public class CreateCategoryRequestDTO : IMapTo<Category>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;
    }
}
