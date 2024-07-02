using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Territories
{
    [TransientService]
    public class TerritoryService : DeletableEntityService<Territory>, ITerritoryService
    {
        public TerritoryService(IDeletableEntityRepository<Territory> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
