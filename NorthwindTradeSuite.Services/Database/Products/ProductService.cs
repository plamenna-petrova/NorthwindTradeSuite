using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Products
{
    [TransientService]
    public class ProductService : DeletableEntityService<Product>, IProductService
    {
        public ProductService(IDeletableEntityRepository<Product> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
