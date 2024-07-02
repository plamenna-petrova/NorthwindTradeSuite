using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Abstraction;

namespace NorthwindTradeSuite.Services.Database.EmployeeTerritories
{
    [TransientService]
    public class EmployeeTerritoryService : BaseService<EmployeeTerritory>, IEmployeeTerritoryService
    {
        public EmployeeTerritoryService(IBaseRepository<EmployeeTerritory> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
