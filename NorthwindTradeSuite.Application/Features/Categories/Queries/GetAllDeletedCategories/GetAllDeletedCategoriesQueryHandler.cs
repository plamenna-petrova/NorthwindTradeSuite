using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllDeletedCategories
{
    public class GetAllDeletedCategoriesQueryHandler : IQueryHandler<GetAllDeletedCategoriesQuery, List<CategoryDetailsResponseDTO>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllDeletedCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;        
        }

        public Task<List<CategoryDetailsResponseDTO>> Handle(GetAllDeletedCategoriesQuery getAllDeletedCategoriesQuery, CancellationToken cancellationToken)
        {
            var deletedCategoriesResponse = _categoryService.GetAllWithOptionalDeletionFlagAsync<CategoryDetailsResponseDTO>(isDeletedFlag: true, asNoTracking: true);
            return deletedCategoriesResponse;
        }
    }
}
