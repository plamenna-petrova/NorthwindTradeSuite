using NorthwindTradeSuite.Domain.Abstraction;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity<string>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsQueryable();

        IQueryable<TEntity> GetAllAsNoTracking();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression);

        TEntity GetById(string id);

        Task<TEntity?> GetByIdAsync(string id);

        IQueryable<TEntity> GetByIdAsQueryable(string id);

        Task<TEntity?> GetFirstOrDefaultByIdAsync(string id);

        Task<TEntity?> GetSingleOrDefaultByIdAsync(string id);

        void Add(TEntity entityToAdd);

        Task AddAsync(TEntity entityToAdd);

        Task<TEntity> AddAsyncAndReturnEntityFromEntry(TEntity entityToAdd);

        void AddRange(TEntity[] entitiesRangeToAdd);

        Task AddRangeAsync(TEntity[] entitiesRangeToAdd);

        void Update(TEntity entityToUpdate);

        void ReattachAndUpdate(TEntity entityToUpdate);

        TEntity ReattachUpdateAndReturnEntityFromEntry(TEntity entityToUpdate);

        void UpdateRange(TEntity[] entitiesRangeToUpdate);

        void Delete(TEntity entityToDelete);

        TEntity DeleteAndReturnEntityFromEntry(TEntity entityToDelete);

        void DeleteRange(TEntity[] entitiesRangeToDelete);

        bool Exists(IQueryable<TEntity> entities, TEntity entityToFind);

        Task<bool> ExistsAsync(IQueryable<TEntity> entities, TEntity entityToFind);

        void DetachLocalEntity(TEntity entityToDetach);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
