using AutoMapper;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Base;

namespace NorthwindTradeSuite.Services.Database.Suppliers
{
    public class SupplierService : DeletableEntityService<Supplier>, ISupplierService
    {
        public SupplierService(IDeletableEntityRepository<Supplier> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {

        }
    }
}
