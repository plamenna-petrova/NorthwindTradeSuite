using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IDeletableEntityRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> GetAllWithDeletedEntities(bool asNoTracking = false);

        Task<List<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag, bool asNoTracking = false);

        IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false);

        void HardDelete(TEntity entityToHardDelete);

        TEntity HardDeleteAndReturnEntityFromEntry(TEntity entityToHardDelete);

        void Restore(TEntity entityToRestore);

        TEntity RestoreAndReturnEntityFromEntry(TEntity entityToRestore);   
    }
}
