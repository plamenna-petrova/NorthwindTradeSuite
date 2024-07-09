using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, CategoryDetailsResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDetailsResponseDTO> Handle(DeleteCategoryCommand deleteCategoryCommand, CancellationToken cancellationToken)
        {
            var deletedCategoryDetailsResponseDTO = await _categoryService.DeleteAndReturnAsync<CategoryDetailsResponseDTO>(
                deleteCategoryCommand.Id, deleteCategoryCommand.CurrentUserId
            );

            return deletedCategoryDetailsResponseDTO;
        }
    }
}
