using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Regions
{
    public class RegionService : DeletableEntityService<Region>, IRegionService
    {
        public RegionService(IDeletableEntityRepository<Region> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
