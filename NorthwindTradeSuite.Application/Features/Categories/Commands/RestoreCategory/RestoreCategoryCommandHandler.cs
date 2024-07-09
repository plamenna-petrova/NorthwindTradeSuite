using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Application.Features.Categories.Commands.DeleteCategory;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommandHandler : ICommandHandler<RestoreCategoryCommand, CategoryDetailsResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public RestoreCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDetailsResponseDTO> Handle(RestoreCategoryCommand restoreCategoryCommand, CancellationToken cancellationToken)
        {
            var restoredCategoryDetailsResponseDTO = await _categoryService.RestoreAndReturnAsync<CategoryDetailsResponseDTO>(
                restoreCategoryCommand.Id, restoreCategoryCommand.CurrentUserId
            );

            return restoredCategoryDetailsResponseDTO;
        }
    }
}
