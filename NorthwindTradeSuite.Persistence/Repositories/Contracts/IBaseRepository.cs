using NorthwindTradeSuite.Domain.Abstraction;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity<string>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsQueryable();

        Task<List<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllAsNoTracking();

        Task<List<TEntity>> GetAllAsNoTrackingAsync();

        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression);

        IQueryable<TEntity> GetAllByConditionAsNoTracking(Expression<Func<TEntity, bool>> filterExpression);

        Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<List<TEntity>> GetAllByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression);

        TEntity? GetById(string id);

        Task<TEntity?> GetByIdAsync(string id);

        IQueryable<TEntity> GetByIdAsQueryable(string id);

        TEntity? GetFirstOrDefaultById(string id);

        Task<TEntity?> GetFirstOrDefaultByIdAsync(string id);

        Task<TEntity?> GetFirstOrDefaultByIdAsNoTrackingAsync(string id);

        Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity?> GetFirstOrDefaultByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression);

        TEntity? GetSingleOrDefaultById(string id);

        Task<TEntity?> GetSingleOrDefaultByIdAsync(string id);

        Task<TEntity?> GetSingleOrDefaultByIdAsNoTrackingAsync(string id);

        Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity?> GetSingleOrDefaultByConditionAsNoTrackingAsync(Expression<Func<TEntity, bool>> filterExpression);

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

        int GetTotalRecords();

        Task<int> GetTotalRecordsAsync();

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IQueryable<TEntity> ExecuteRawSqlQuery(string queryString, params string[] queryParameters);
    }
}
