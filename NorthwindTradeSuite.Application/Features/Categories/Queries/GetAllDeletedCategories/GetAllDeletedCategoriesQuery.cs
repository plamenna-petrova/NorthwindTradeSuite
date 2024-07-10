using NorthwindTradeSuite.Application.Abstraction;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllDeletedCategories
{
    public sealed record GetAllDeletedCategoriesQuery() : CachedQuery<List<CategoryDetailsResponseDTO>>();
}
