using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Employees
{
    [TransientService]
    public class EmployeeService : DeletableEntityService<Employee>, IEmployeeService
    {
        public EmployeeService(IDeletableEntityRepository<Employee> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
