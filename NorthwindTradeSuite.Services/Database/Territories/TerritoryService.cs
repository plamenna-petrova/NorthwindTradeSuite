using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Territories
{
    public class TerritoryService : DeletableEntityService<Territory>, ITerritoryService
    {
        public TerritoryService(IDeletableEntityRepository<Territory> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
