using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Customers
{
    [TransientService]
    public class CustomerService : DeletableEntityService<Customer>, ICustomerService
    {
        public CustomerService(IDeletableEntityRepository<Customer> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
