using System.Linq.Expressions;

namespace NorthwindTradeSuite.Services.Database.Base.Contracts
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool asNoTracking = false);

        IQueryable<TDTO> GetAll<TDTO>(bool asNoTracking = false);

        Task<List<TEntity>> GetAllAsync(bool asNoTracking = false);

        Task<List<TDTO>> GetAllAsync<TDTO>(bool asNoTracking = false);

        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TEntity GetById(string id);

        TDTO GetById<TDTO>(string id);

        Task<TEntity> GetByIdAsync(string id);

        Task<TDTO> GetByIdAsync<TDTO>(string id);

        TEntity? GetFirstOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TDTO? GetFirstOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TDTO?> GetFirstOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TEntity? GetSingleOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TDTO? GetSingleOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TDTO?> GetSingleOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        void Create<TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        TDTO CreateAndReturn<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task CreateAsync<TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task<TDTO> CreateAndReturnAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        void CreateMultiple<TCreateDTO>(List<TCreateDTO> createDTOs);

        Task CreateMultipleAsync<TCreateDTO>(List<TCreateDTO> createDTOs);

        void Update<TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        TDTO UpdateAndReturn<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task UpdateAsync<TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task<TDTO> UpdateAndReturnAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        void UpdateMultiple<TUpdateDTO>(List<TUpdateDTO> updateDTOs);

        Task UpdateMultipleAsync<TUpdateDTO>(List<TUpdateDTO> updateDTOs);

        void Delete(string id, string currentUserId);

        TDTO DeleteAndReturn<TDTO>(string id, string currentUserId);

        Task DeleteAsync(string id, string currentUserId);

        Task<TDTO> DeleteAndReturnAsync<TDTO>(string id, string currentUserId);

        void DeleteRange<TDeleteDTO>(List<TDeleteDTO> deleteDTOs);

        Task DeleteRangeAsync<TDeleteDTO>(List<TDeleteDTO> deleteDTOs);

        bool Exists(IQueryable<TEntity> entities, TEntity entityToFind);

        bool Exists<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind);

        Task<bool> ExistsAsync(IQueryable<TEntity> entities, TEntity entityToFind);

        Task<bool> ExistsAsync<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind);

        int GetTotalRecords();

        Task<int> GetTotalRecordsAsync();

        IQueryable<TEntity> BuildQueryFromRawSql(string queryString, params object[] queryParameters);

        IQueryable<TDTO> BuildQueryFromRawSql<TDTO>(string queryString, params object[] queryParameters);

        int ExecuteSqlRawQuery(string queryString, params object[] queryParameters);

        Task<int> ExecuteSqlRawQueryAsync(string queryString, params object[] queryParameters);
    }
}
