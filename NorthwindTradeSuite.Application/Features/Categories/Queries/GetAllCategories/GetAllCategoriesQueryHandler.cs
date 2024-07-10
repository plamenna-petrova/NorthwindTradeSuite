using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<CategoryResponseDTO>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<CategoryResponseDTO>> Handle(GetAllCategoriesQuery getAllCategoriesQuery, CancellationToken cancellationToken)
        {
            var allCategoriesResponse = await _categoryService.GetAllAsync<CategoryResponseDTO>(asNoTracking: true);
            return allCategoriesResponse;
        }
    }
}
