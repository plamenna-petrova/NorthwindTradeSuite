﻿using NorthwindTradeSuite.Common.Enums;
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

        IQueryable<TDTO> GetAllByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TDTO>> GetAllByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filterExpression, FilteringSource filteringSource, bool asNoTracking = false, Expression<Func<TEntity, object>>[] includeProperties = null!);

        Task<List<TDTO>> GetAllFilteredAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, FilteringSource filteringSource, bool asNoTracking = false, Expression<Func<TEntity, object>>[] includeProperties = null!);

        TEntity GetById(string id);

        TDTO GetById<TDTO>(string id);

        Task<TEntity> GetByIdAsync(string id);

        Task<TDTO> GetByIdAsync<TDTO>(string id);

        TEntity? GetFirstOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TDTO? GetFirstOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TDTO?> GetFirstOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetFirstOrDefaultIncludingAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TDTO?> GetFirstOrDefaultIncludingAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity? GetSingleOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        TDTO? GetSingleOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TDTO?> GetSingleOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<TEntity?> GetSingleOrDefaultIncludingAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TDTO?> GetSingleOrDefaultIncludingAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includeProperties);

        void Create<TCreateDTO>(TCreateDTO createDTO, string? currentUserId = null);

        TDTO CreateAndReturn<TDTO, TCreateDTO>(TCreateDTO createDTO, string? currentUserId = null);

        Task CreateAsync<TCreateDTO>(TCreateDTO createDTO, string? currentUserId = null);

        Task<TDTO> CreateAndReturnAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string? currentUserId = null);

        void CreateMultiple<TCreateDTO>(List<TCreateDTO> createDTOs, string? currentUserId = null);

        Task CreateMultipleAsync<TCreateDTO>(List<TCreateDTO> createDTOs, string? currentUserId = null);

        void Update<TUpdateDTO>(string id, TUpdateDTO updateDTO, string? currentUserId = null);

        TDTO UpdateAndReturn<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string? currentUserId = null);

        Task UpdateAsync<TUpdateDTO>(string id, TUpdateDTO updateDTO, string? currentUserId = null);

        Task<TDTO> UpdateAndReturnAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string? currentUserId = null);

        void UpdateMultiple<TUpdateDTO>(List<TUpdateDTO> updateDTOs, string? currentUserId = null);

        Task UpdateMultipleAsync<TUpdateDTO>(List<TUpdateDTO> updateDTOs, string? currentUserId = null);

        void Delete(string id, string? currentUserId = null);

        TDTO DeleteAndReturn<TDTO>(string id, string? currentUserId = null);

        Task DeleteAsync(string id, string? currentUserId = null);

        Task<TDTO> DeleteAndReturnAsync<TDTO>(string id, string? currentUserId = null);

        void DeleteRange<TDeleteDTO>(List<TDeleteDTO> deleteDTOs, string? currentUserId = null);

        Task DeleteRangeAsync<TDeleteDTO>(List<TDeleteDTO> deleteDTOs, string? currentUserId = null);

        bool Exists(IQueryable<TEntity> entities, TEntity entityToFind);

        bool Exists<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind);

        bool ExistsBy(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        Task<bool> ExistsAsync(IQueryable<TEntity> entities, TEntity entityToFind);

        Task<bool> ExistsAsync<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind);

        Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false);

        int GetTotalRecords();

        Task<int> GetTotalRecordsAsync();

        IQueryable<TEntity> BuildQueryFromRawSql(string queryString, params object[] queryParameters);

        IQueryable<TDTO> BuildQueryFromRawSql<TDTO>(string queryString, params object[] queryParameters);

        int ExecuteSqlRawQuery(string queryString, params object[] queryParameters);

        Task<int> ExecuteSqlRawQueryAsync(string queryString, params object[] queryParameters);
    }
}
