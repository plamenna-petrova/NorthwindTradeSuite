using NorthwindTradeSuite.Application.Abstraction;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryDetails
{
    public sealed record GetCategoryDetailsQuery(string Id) : CachedQuery<CategoryDetailsResponseDTO>;
}
