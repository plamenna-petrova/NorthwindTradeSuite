using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        protected ApplicationDbContext DbContext { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false) => asNoTracking ? DbSet.AsNoTracking() : DbSet;

        public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking = false) => await GetAll(asNoTracking).ToListAsync();

        public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => asNoTracking ? GetAll().AsQueryable().Where(filterExpression).AsNoTracking() 
                            : GetAll().AsQueryable().Where(filterExpression);

        public virtual async Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => asNoTracking ? await GetAll().AsQueryable().Where(filterExpression).AsNoTracking().ToListAsync()
                            : await GetAll().AsQueryable().Where(filterExpression).ToListAsync();

        public virtual TEntity? GetById(string id) => DbSet.Find(id);

        public virtual async Task<TEntity?> GetByIdAsync(string id) => await DbSet.FindAsync(id).AsTask();

        public virtual TEntity? GetFirstOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => GetAll(asNoTracking).FirstOrDefault(filterExpression);

        public virtual async Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => await GetAll(asNoTracking).FirstOrDefaultAsync(filterExpression);

        public virtual TEntity? GetSingleOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => GetAll(asNoTracking).SingleOrDefault(filterExpression);

        public virtual async Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => await GetAll(asNoTracking).SingleOrDefaultAsync(filterExpression);

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

        public virtual void DetachLocalEntity<TLocalEntity>(TLocalEntity entityToDetach) where TLocalEntity : BaseEntity<string>
        {
            LocalView<TLocalEntity> entitiesLocalView = DbContext.Set<TLocalEntity>().Local;
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
