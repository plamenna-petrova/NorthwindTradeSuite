using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, CategoryResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryResponseDTO> Handle(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken)
        {
            var updatedCategoryResponseDTO = await _categoryService.UpdateAndReturnAsync<CategoryResponseDTO, UpdateCategoryRequestDTO>(
                updateCategoryCommand.Id, updateCategoryCommand.UpdateCategoryRequestDTO, updateCategoryCommand.CurrentUserId
            );

            return updatedCategoryResponseDTO;
        }
    }
}
