using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(string Id, string? CurrentUserId) : ICommand<CategoryDetailsResponseDTO>;
}
