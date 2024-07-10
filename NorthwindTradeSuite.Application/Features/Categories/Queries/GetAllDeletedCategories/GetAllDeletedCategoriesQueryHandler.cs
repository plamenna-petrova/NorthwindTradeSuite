using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllDeletedCategories
{
    public class GetAllDeletedCategoriesQueryHandler : IQueryHandler<GetAllDeletedCategoriesQuery, List<CategoryResponseDTO>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllDeletedCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;        
        }

        public Task<List<CategoryResponseDTO>> Handle(GetAllDeletedCategoriesQuery getAllDeletedCategoriesQuery, CancellationToken cancellationToken)
        {
            var deletedCategories = _categoryService.GetAllWithOptionalDeletionFlagAsync<CategoryResponseDTO>(isDeletedFlag: true, asNoTracking: true);
            return deletedCategories;
        }
    }
}
