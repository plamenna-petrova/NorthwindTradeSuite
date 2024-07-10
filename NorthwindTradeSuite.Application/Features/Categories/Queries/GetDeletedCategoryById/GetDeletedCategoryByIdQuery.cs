using NorthwindTradeSuite.Application.Abstraction;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetDeletedCategoryById
{
    public sealed record GetDeletedCategoryByIdQuery(string Id) : CachedQuery<CategoryDetailsResponseDTO>;
}
