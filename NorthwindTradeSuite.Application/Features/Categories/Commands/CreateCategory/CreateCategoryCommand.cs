using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(CreateCategoryRequestDTO CreateCategoryRequestDTO, string? CurrentUserId) : ICommand<RequestResult<CategoryResponseDTO>>;
}
