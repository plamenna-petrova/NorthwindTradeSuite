using NorthwindTradeSuite.Application.Contracts;
using NorthwindTradeSuite.DTOs.Responses.Categories;
using NorthwindTradeSuite.Services.Database.Categories;

namespace NorthwindTradeSuite.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryResponseDTO>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryResponseDTO> Handle(GetCategoryByIdQuery getCategoryByIdQuery, CancellationToken cancellationToken)
        {
            var categoryById = await _categoryService.GetByIdAsync<CategoryResponseDTO>(getCategoryByIdQuery.Id);
            return categoryById;
        }
    }
}
