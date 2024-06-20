using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;

namespace NorthwindTradeSuite.Persistence.Repositories.Implementation
{
    public class DeletableEntityRepository<TEntity> : BaseRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        public DeletableEntityRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {

        }

        public override IQueryable<TEntity> GetAll(bool asNoTracking = false) => base.GetAll(asNoTracking).Where(e => !e.IsDeleted);

        public override void Delete(TEntity entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            entityToDelete.DeletedAt = DateTime.UtcNow;
            base.ReattachAndUpdate(entityToDelete);
        }

        public IQueryable<TEntity> GetAllWithDeletedEntities() => base.GetAll().IgnoreQueryFilters();

        public IQueryable<TEntity> GetAllAsNoTrackingWithDeletedEntities() => base.GetAll(asNoTracking: true).IgnoreQueryFilters();

        public async Task<List<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag = false)
            => await base.GetAllByConditionAsync(e => e.IsDeleted == isDeletedFlag);

        public async Task<List<TEntity>> GetAllAsNoTrackingWithOptionalDeletionFlagAsync(bool isDeletedFlag = false)
            => await base.GetAllByConditionAsync(e => e.IsDeleted == isDeletedFlag, asNoTracking: true);

        public IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag = false)
            => base.GetAllByCondition(e => e.Id == id && e.IsDeleted == isDeletedFlag);

        public async Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag = false)
            => await base.GetAllByCondition(e => e.IsDeleted == isDeletedFlag, asNoTracking: true)
                         .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag = false)
            => await base.GetAllByCondition(e => e.IsDeleted == isDeletedFlag, asNoTracking: true)
                         .SingleOrDefaultAsync(e => e.Id == id);

        public void HardDelete(TEntity entityToHardDelete) => base.Delete(entityToHardDelete);

        public TEntity HardDeleteAndReturnEntityFromEntry(TEntity entityToHardDelete) 
            => base.DeleteAndReturnEntityFromEntry(entityToHardDelete);

        public void Restore(TEntity entityToRestore) 
        {
            entityToRestore.IsDeleted = false;
            entityToRestore.DeletedAt = null;
            base.ReattachAndUpdate(entityToRestore);
        }

        public TEntity RestoreAndReturnEntityFromEntry(TEntity entityToRestore) 
        {
            entityToRestore.IsDeleted = false;
            entityToRestore.DeletedAt = null;
            return base.ReattachUpdateAndReturnEntityFromEntry(entityToRestore);
        }
    }
}
