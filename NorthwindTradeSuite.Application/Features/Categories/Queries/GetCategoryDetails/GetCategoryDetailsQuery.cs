using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryDetails
{
    public sealed record GetCategoryDetailsQuery(string Id) : IQuery<CategoryDetailsResponseDTO>;
}
