using AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Abstraction;

namespace NorthwindTradeSuite.Services.Database.OrderDetails
{
    public class OrderDetailsService : BaseService<Domain.Entities.OrderDetails>, IOrderDetailsService
    {
        public OrderDetailsService(IBaseRepository<Domain.Entities.OrderDetails> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
