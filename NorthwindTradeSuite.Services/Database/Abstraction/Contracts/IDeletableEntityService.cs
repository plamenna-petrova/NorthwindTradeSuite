using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Services.Database.Base.Contracts
{
    public interface IDeletableEntityService<TEntity> : IBaseService<TEntity> where TEntity : BaseDeletableEntity
    {
        IQueryable<TEntity> GetAllWithDeleted(bool asNoTracking = false);

        IQueryable<TDTO> GetAllWithDeleted<TDTO>(bool asNoTracking = false);

        Task<List<TEntity>> GetAllWithDeletedAsync(bool asNoTracking = false);

        Task<List<TDTO>> GetAllWithDeletedAsync<TDTO>(bool asNoTracking = false);

        Task<List<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag = false, bool asNoTracking = false);

        Task<List<TDTO>> GetAllWithOptionalDeletionFlagAsync<TDTO>(bool isDeletedFlag = false, bool asNoTracking = false);

        IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag, bool asNoTracking = false);

        IQueryable<TDTO> GetByIdWithOptionalDeletionFlagAsQueryable<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TDTO?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false);

        Task<TDTO?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false);

        void HardDelete(string id);

        TDTO HardDelete<TDTO>(string id);

        Task HardDeleteAsync(string id);

        Task<TDTO> HardDeleteAsync<TDTO>(string id);

        void Restore(string id);

        TDTO Restore<TDTO>(string id);

        Task RestoreAsync(string id);

        Task<TDTO> RestoreAsync<TDTO>(string id);
    }
}
