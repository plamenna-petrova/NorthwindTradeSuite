using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Services.Database.Base.Contracts
{
    public interface IDeletableEntityService<TEntity> : IBaseService<TEntity> where TEntity : BaseDeletableEntity<string>
    {
        Task<List<TDTO>> GetAllWithDeletedAsync<TDTO>(bool asNoTracking = false);

        Task<TDTO> HardDeleteAsync<TDTO>(string id);

        Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId);
    }
}
