using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Customers
{
    public class CustomerService : DeletableEntityService<Customer>, ICustomerService
    {
        public CustomerService(IDeletableEntityRepository<Customer> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
