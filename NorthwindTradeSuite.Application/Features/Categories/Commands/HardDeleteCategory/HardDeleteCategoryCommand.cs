using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.HardDeleteCategory
{
    public sealed record HardDeleteCategoryCommand(string Id) : ICommand<CategoryConciseInformationResponseDTO>;
}
