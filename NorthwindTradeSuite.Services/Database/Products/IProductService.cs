using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Services.Database.Base.Contracts;

namespace NorthwindTradeSuite.Services.Database.Products
{
    public interface IProductService : IDeletableEntityService<Product>
    {
    }
}
