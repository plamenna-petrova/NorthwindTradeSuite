using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IDeletableEntityRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        IQueryable<TEntity> GetAllWithDeletedEntities();

        IQueryable<TEntity> GetAllAsNoTrackingWithDeletedEntities();

        Task<List<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag);

        Task<List<TEntity>> GetAllAsNoTrackingWithOptionalDeletionFlagAsync(bool isDeletedFlag);

        IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag);

        Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag);

        Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag);

        void HardDelete(TEntity entityToHardDelete);

        TEntity HardDeleteAndReturnEntityFromEntry(TEntity entityToHardDelete);

        void Restore(TEntity entityToRestore);

        TEntity RestoreAndReturnEntityFromEntry(TEntity entityToRestore);   
    }
}
