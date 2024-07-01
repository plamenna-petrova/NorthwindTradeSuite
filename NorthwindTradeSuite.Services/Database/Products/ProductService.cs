using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Products
{
    public class ProductService : DeletableEntityService<Product>, IProductService
    {
        public ProductService(IDeletableEntityRepository<Product> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
