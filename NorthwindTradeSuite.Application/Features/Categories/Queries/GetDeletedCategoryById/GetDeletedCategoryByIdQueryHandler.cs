using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetDeletedCategoryById
{
    public class GetDeletedCategoryByIdQueryHandler : IQueryHandler<GetDeletedCategoryByIdQuery, CategoryDetailsResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public GetDeletedCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDetailsResponseDTO> Handle(GetDeletedCategoryByIdQuery getDeletedCategoryByIdQuery, CancellationToken cancellationToken)
        {
            var deletedCategoryByIdDetailsResponseDTO = await _categoryService.GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync<CategoryDetailsResponseDTO>(
                getDeletedCategoryByIdQuery.Id, isDeletedFlag: true, asNoTracking: true
            );

            return deletedCategoryByIdDetailsResponseDTO!;
        }
    }
}
