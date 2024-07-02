using AutoMapper;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Shippers
{
    [TransientService]
    public class ShipperService : DeletableEntityService<Shipper>, IShipperService
    {
        public ShipperService(IDeletableEntityRepository<Shipper> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
