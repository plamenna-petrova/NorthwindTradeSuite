using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Categories
{
    public class CategoryService : DeletableEntityService<Category>, ICategoryService
    {
        public CategoryService(IDeletableEntityRepository<Category> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
