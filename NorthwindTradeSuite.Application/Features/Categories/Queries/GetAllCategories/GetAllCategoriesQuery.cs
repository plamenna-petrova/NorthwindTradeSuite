using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IQuery<List<GetCategoriesResponseDTO>>;
}
