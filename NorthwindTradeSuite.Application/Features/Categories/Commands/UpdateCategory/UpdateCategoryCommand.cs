using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(string Id, UpdateCategoryRequestDTO UpdateCategoryRequestDTO, string? CurrentUserId) : ICommand<CategoryResponseDTO>;
}
