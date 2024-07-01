using System.Linq.Expressions;
using System.Threading.Tasks;

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

        void Create<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        TDTO CreateAndReturn<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        Task<TDTO> CreateAndReturnAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId);

        void Update<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        TDTO UpdateAndReturn<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        Task<TDTO> UpdateAndReturnAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId);

        void Delete<TDTO>(string id, string currentUserId);

        TDTO DeleteAndReturn<TDTO>(string id, string currentUserId);

        Task DeleteAsync<TDTO>(string id, string currentUserId);

        Task<TDTO> DeleteAndReturnAsync<TDTO>(string id, string currentUserId);
    }
}
