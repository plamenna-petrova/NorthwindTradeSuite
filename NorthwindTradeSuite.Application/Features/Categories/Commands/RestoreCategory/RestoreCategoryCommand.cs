using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.RestoreCategory
{
    public sealed record RestoreCategoryCommand(string Id, string? CurrentUserId) : ICommand<CategoryDetailsResponseDTO>;
}
