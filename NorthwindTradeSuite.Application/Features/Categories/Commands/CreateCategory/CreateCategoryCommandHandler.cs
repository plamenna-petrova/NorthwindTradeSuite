using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.Common.Results;
using NorthwindTradeSuite.DTOs.Requests.Categories;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, RequestResult<CategoryResponseDTO>>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<RequestResult<CategoryResponseDTO>> Handle(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
        {
            if (await _categoryService.ExistsByAsync(cat => cat.Name == createCategoryCommand.CreateCategoryRequestDTO.Name, asNoTracking: true))
            {
                return RequestResult<CategoryResponseDTO>.Failure($"A category with the name: '{createCategoryCommand.CreateCategoryRequestDTO.Name}' already exists.");
            }

            var createdCategoryResponseDTO = await _categoryService.CreateAndReturnAsync<CategoryResponseDTO, CreateCategoryRequestDTO>(
                createCategoryCommand.CreateCategoryRequestDTO, createCategoryCommand.CurrentUserId
            );

            return RequestResult<CategoryResponseDTO>.Success(createdCategoryResponseDTO);
        }
    }
}
