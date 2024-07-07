using Castle.Core.Resource;
using NorthwindTradeSuite.Common.Enums;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool asNoTracking = false);

        Task<List<TEntity>> GetAllAsync(bool asNoTracking = false);

        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filterExpression, FilteringSource filteringSource, bool asNoTracking = false, Expression<Func<TEntity, object>>[] includeProperties = null!);

        TEntity? GetById(string id);

        Task<TEntity?> GetByIdAsync(string id);

        TEntity? GetFirstOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultIncludingAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity? GetSingleOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultIncludingAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        void Add(TEntity entityToAdd);

        TEntity AddAndReturnEntityFromEntry(TEntity entityToAdd);

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

        IQueryable<TEntity> BuildQueryFromRawSql(string queryString, params object[] queryParameters);

        int ExecuteSqlRawQuery(string queryString, params object[] queryParameters);

        Task<int> ExecuteSqlRawQueryAsync(string queryString, params object[] queryParameters);
    }
}