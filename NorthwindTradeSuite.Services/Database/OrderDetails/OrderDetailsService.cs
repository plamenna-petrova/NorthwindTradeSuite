using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Abstraction;

namespace NorthwindTradeSuite.Services.Database.OrderDetails
{
    [TransientService]
    public class OrderDetailsService : BaseService<Domain.Entities.OrderDetails>, IOrderDetailsService
    {
        public OrderDetailsService(IBaseRepository<Domain.Entities.OrderDetails> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
