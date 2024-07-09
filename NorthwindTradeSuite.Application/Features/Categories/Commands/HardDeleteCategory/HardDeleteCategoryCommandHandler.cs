using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.HardDeleteCategory
{
    public class HardDeleteCategoryCommandHandler : ICommandHandler<HardDeleteCategoryCommand, CategoryConciseInformationResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public HardDeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryConciseInformationResponseDTO> Handle(HardDeleteCategoryCommand hardDeleteCategoryCommand, CancellationToken cancellationToken)
        {
            var hardDeletedCategoryConciseInformationResponseDTO = await _categoryService.HardDeleteAndReturnAsync<CategoryConciseInformationResponseDTO>(hardDeleteCategoryCommand.Id);
            return hardDeletedCategoryConciseInformationResponseDTO;
        }
    }
}
