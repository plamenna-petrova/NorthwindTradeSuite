using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Orders
{
    public class OrderService : DeletableEntityService<Order>, IOrderService
    {
        public OrderService(IDeletableEntityRepository<Order> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
