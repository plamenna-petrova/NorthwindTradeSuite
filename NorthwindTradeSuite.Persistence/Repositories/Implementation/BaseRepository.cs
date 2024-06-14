using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<string>
    {
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        protected ApplicationDbContext DbContext { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public virtual IQueryable<TEntity> GetAll() => DbSet;

        public virtual IQueryable<TEntity> GetAllAsQueryable() => DbSet.AsQueryable();

        public virtual async Task<List<TEntity>> GetAllAsync() => await GetAllAsQueryable().ToListAsync();

        public virtual IQueryable<TEntity> GetAllAsNoTracking() => DbSet.AsNoTracking();

        public virtual async Task<List<TEntity>> GetAllAsNoTrackingAsync()
            => await GetAllAsNoTracking().ToListAsync();

        public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression)
            => GetAllAsQueryable().Where(filterExpression);

        public virtual IQueryable<TEntity> GetAllByConditionAsNoTracking(Expression<Func<TEntity, bool>> filterExpression)
            => GetAllAsQueryable().Where(filterExpression).AsNoTracking();

        public virtual async Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression)
            => await GetAllAsQueryable().Where(filterExpression).ToListAsync();

        public virtual async Task<List<TEntity>> GetAllByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression)
            => await GetAllAsQueryable().Where(filterExpression).AsNoTracking().ToListAsync();

        public virtual TEntity? GetById(string id) => DbSet.Find(id);

        public virtual async Task<TEntity?> GetByIdAsync(string id) => await DbSet.FindAsync(id);

        public virtual IQueryable<TEntity> GetByIdAsQueryable(string id) => DbSet.Where(e => e.Id == id);

        public virtual TEntity? GetFirstOrDefaultById(string id) => GetAll().FirstOrDefault(e => e.Id == id);

        public virtual async Task<TEntity?> GetFirstOrDefaultByIdAsync(string id)
           => await GetAll().FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity?> GetFirstOrDefaultByIdAsNoTrackingAsync(string id)
            => await GetAllAsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression)
           => await GetAll().FirstOrDefaultAsync(filterExpression);

        public virtual async Task<TEntity?> GetFirstOrDefaultByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression)
           => await GetAllAsNoTracking().FirstOrDefaultAsync(filterExpression);

        public virtual TEntity? GetSingleOrDefaultById(string id) => GetAll().SingleOrDefault(e => e.Id == id);

        public virtual async Task<TEntity?> GetSingleOrDefaultByIdAsync(string id)
           => await GetAll().SingleOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity?> GetSingleOrDefaultByIdAsNoTrackingAsync(string id)
            => await GetAllAsNoTracking().SingleOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression)
            => await GetAll().SingleOrDefaultAsync(filterExpression);

        public virtual async Task<TEntity?> GetSingleOrDefaultByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression)
            => await GetAllAsNoTracking().SingleOrDefaultAsync(filterExpression);

        public virtual void Add(TEntity entityToAdd) => DbSet.Add(entityToAdd);

        public virtual async Task AddAsync(TEntity entityToAdd) => await DbSet.AddAsync(entityToAdd).AsTask();

        public virtual async Task<TEntity> AddAsyncAndReturnEntityFromEntry(TEntity entityToAdd)
        {
            EntityEntry<TEntity>? addedEntityEntry = await DbSet.AddAsync(entityToAdd).AsTask();
            return addedEntityEntry.Entity;
        }

        public virtual void AddRange(TEntity[] entitiesRangeToAdd) => DbSet.AddRange(entitiesRangeToAdd);

        public virtual async Task AddRangeAsync(TEntity[] entitiesRangeToAdd) => await DbSet.AddRangeAsync(entitiesRangeToAdd);

        public virtual void Update(TEntity entityToUpdate) => DbSet.Update(entityToUpdate);

        public virtual void ReattachAndUpdate(TEntity entityToUpdate) 
        {
            EntityEntry<TEntity>? updatedEntityEntry = DbContext.Entry(entityToUpdate);

            if (updatedEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }

            updatedEntityEntry.State = EntityState.Modified;      
        }

        public virtual TEntity ReattachUpdateAndReturnEntityFromEntry(TEntity entityToUpdate)
        {
            EntityEntry<TEntity>? updatedEntityEntry = DbContext.Entry(entityToUpdate);

            if (updatedEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }

            updatedEntityEntry.State = EntityState.Modified;

            return updatedEntityEntry.Entity;
        }

        public virtual void UpdateRange(TEntity[] entitiesRangeToUpdate) => DbSet.UpdateRange(entitiesRangeToUpdate);
        
        public virtual void Delete(TEntity entityToDelete) => DbSet.Remove(entityToDelete);

        public virtual TEntity DeleteAndReturnEntityFromEntry(TEntity entityToDelete)
        {
            EntityEntry<TEntity>? deletedEntityEntry = DbSet.Remove(entityToDelete);
            return deletedEntityEntry.Entity;
        }

        public virtual void DeleteRange(TEntity[] entitiesRangeToDelete) => DbSet.RemoveRange(entitiesRangeToDelete);

        public virtual bool Exists(IQueryable<TEntity> entities, TEntity entityToFind) 
            => entities.Any(e => e == entityToFind);

        public virtual async Task<bool> ExistsAsync(IQueryable<TEntity> entities, TEntity entityToFind)
            => await entities.AnyAsync(e => e == entityToFind);

        public virtual void DetachLocalEntity(TEntity entityToDetach)
        {
            LocalView<TEntity> entitiesLocalView = DbSet.Local;
            var localEntityToDetach = entitiesLocalView.FirstOrDefault(entry => entry.Id.Equals(entityToDetach.Id));

            if (localEntityToDetach != null)
            {
                DbContext.Entry(localEntityToDetach).State = EntityState.Detached;
            }

            DbContext.Entry(localEntityToDetach!).State = EntityState.Modified;
        }

        public int GetTotalRecords() => DbSet.Count();

        public async Task<int> GetTotalRecordsAsync() => await DbSet.CountAsync();

        public int SaveChanges() => DbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

        public virtual IQueryable<TEntity> BuildQueryFromRawSql(string queryString, params object[] queryParameters)
            => DbSet.FromSqlRaw(queryString, queryParameters);

        public virtual int ExecuteSqlRawQuery(string queryString, params object[] queryParameters)
            => DbContext.Database.ExecuteSqlRaw(queryString, queryParameters);

        public virtual async Task<int> ExecuteSqlRawQueryAsync(string queryString, params object[] queryParameters)
            => await DbContext.Database.ExecuteSqlRawAsync(queryString, queryParameters);

        public void Dispose()
        {
            DisposeApplicationDbContext(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeApplicationDbContext(bool disposing)
        {
            if (disposing)
            {
                DbContext?.Dispose();
            }
        }
    }
}
