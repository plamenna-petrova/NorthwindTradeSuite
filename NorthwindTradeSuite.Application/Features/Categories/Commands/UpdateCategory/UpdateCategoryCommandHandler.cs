using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;
using static NorthwindTradeSuite.Common.GlobalConstants.HttpConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Result<CategoryResponseDTO>>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Result<CategoryResponseDTO>> Handle(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken)
        {
            if (await _categoryService.GetByIdWithOptionalDeletionFlagAsQueryable(
                updateCategoryCommand.Id, isDeletedFlag: false, asNoTracking: true).FirstOrDefaultAsync() == null)
            {
                return Result<CategoryResponseDTO>.Failure("Failed update: " + string.Format(ENTITY_BY_ID_NOT_FOUND_RESULT, SINGLE_CATEGORY_NAME, updateCategoryCommand.Id));
            }

            var updatedCategoryResponseDTO = await _categoryService.UpdateAndReturnAsync<CategoryResponseDTO, UpdateCategoryRequestDTO>(
                updateCategoryCommand.Id, updateCategoryCommand.UpdateCategoryRequestDTO, updateCategoryCommand.CurrentUserId
            );

            return Result<CategoryResponseDTO>.Success(updatedCategoryResponseDTO)!;
        }
    }
}
