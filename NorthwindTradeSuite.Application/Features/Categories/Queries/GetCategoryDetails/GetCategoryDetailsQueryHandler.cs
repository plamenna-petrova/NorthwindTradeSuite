using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IQueryHandler<GetCategoryDetailsQuery, CategoryDetailsResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryDetailsQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDetailsResponseDTO> Handle(GetCategoryDetailsQuery getCategoryDetailsQuery, CancellationToken cancellationToken)
        {
            var categoryDetails = await _categoryService.GetByIdAsync<CategoryDetailsResponseDTO>(getCategoryDetailsQuery.Id);
            return categoryDetails;
        }
    }
}
