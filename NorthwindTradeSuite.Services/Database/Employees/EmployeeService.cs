using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Employees
{
    public class EmployeeService : DeletableEntityService<Employee>, IEmployeeService
    {
        public EmployeeService(IDeletableEntityRepository<Employee> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
