using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IDeletableEntityRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        IQueryable<TEntity> GetAllWithDeletedEntities();

        IQueryable<TEntity> GetAllAsNoTrackingWithDeletedEntities();

        Task<IEnumerable<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag);

        IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag);

        Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag);

        Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag);

        void HardDelete(TEntity entityToHardDelete);

        TEntity HardDeleteAndReturnEntityFromEntry(TEntity entityToHardDelete);

        void Restore(TEntity entityToRestore);

        TEntity RestoreAndReturnEntityFromEntry(TEntity entityToRestore);   
    }
}
