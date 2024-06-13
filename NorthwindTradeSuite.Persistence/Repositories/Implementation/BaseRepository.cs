using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            DbSet = ApplicationDbContext.Set<TEntity>();
        }

        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public virtual IQueryable<TEntity> GetAll() => DbSet;

        public virtual IQueryable<TEntity> GetAllAsNoTracking() => DbSet.AsNoTracking();

        public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression)
            => DbSet.Where(filterExpression);

        public virtual TEntity GetById(string id) => DbSet.Find(id)!;

        public virtual void Add(TEntity entityToAdd) => DbSet.Add(entityToAdd);

        public virtual void AddRange(TEntity[] entitiesRangeToAdd) => DbSet.AddRange(entitiesRangeToAdd);

        public virtual void Update(TEntity entityToUpdate) => DbSet.Update(entityToUpdate);

        public virtual void UpdateRange(TEntity[] entitiesRangeToUpdate) => DbSet.UpdateRange(entitiesRangeToUpdate);
        
        public virtual void Delete(TEntity entityToDelete) => DbSet.Remove(entityToDelete);

        public void DeleteRange(TEntity[] entitiesRangeToDelete) => DbSet.RemoveRange(entitiesRangeToDelete);

        public virtual bool Exists(IQueryable<TEntity> entities, TEntity entityToFind) 
            => entities.Any(e => e == entityToFind);

        public void Dispose()
        {
            DisposeApplicationDbContext(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeApplicationDbContext(bool disposing)
        {
            if (disposing)
            {
                ApplicationDbContext?.Dispose();
            }
        }
    }
}
