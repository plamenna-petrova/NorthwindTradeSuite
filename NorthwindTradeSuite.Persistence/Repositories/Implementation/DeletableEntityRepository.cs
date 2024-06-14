using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;

namespace NorthwindTradeSuite.Persistence.Repositories.Implementation
{
    public abstract class DeletableEntityRepository<TEntity> : BaseRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        public DeletableEntityRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {

        }

        public override IQueryable<TEntity> GetAll() => base.GetAll().Where(e => !e.IsDeleted);

        public override IQueryable<TEntity> GetAllAsNoTracking() => base.GetAllAsNoTracking().Where(e => !e.IsDeleted);

        public override void Delete(TEntity entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            entityToDelete.DeletedAt = DateTime.UtcNow;
            base.ReattachAndUpdate(entityToDelete);
        }

        public IQueryable<TEntity> GetAllWithDeletedEntities() => base.GetAll().IgnoreQueryFilters();

        public IQueryable<TEntity> GetAllAsNoTrackingWithDeletedEntities() => base.GetAllAsNoTracking().IgnoreQueryFilters();

        public async Task<IEnumerable<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag = false)
            => await base.GetAll()
                         .Where(e => e.IsDeleted == isDeletedFlag)
                         .AsNoTracking()
                         .ToListAsync();

        public IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag = false)
            => base.GetAll().Where(e => e.Id == id && e.IsDeleted == isDeletedFlag);

        public async Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag = false)
            => await base.GetAll()
                         .Where(e => e.IsDeleted == isDeletedFlag)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag = false)
            => await base.GetAll()
                         .Where(e => e.IsDeleted == isDeletedFlag)
                         .AsNoTracking()
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
