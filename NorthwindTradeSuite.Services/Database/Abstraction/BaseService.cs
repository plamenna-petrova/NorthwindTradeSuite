using AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Mappers;
using NorthwindTradeSuite.Services.Database.Base.Contracts;
using System.Linq.Expressions;
using static NorthwindTradeSuite.Common.GlobalConstants.ExceptionMessages;

namespace NorthwindTradeSuite.Services.Database.Abstraction
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        protected readonly IMapper Mapper;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            BaseRepository = baseRepository;
            Mapper = mapper;
        }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false) => BaseRepository.GetAll(asNoTracking);

        public virtual IQueryable<TDTO> GetAll<TDTO>(bool asNoTracking = false)
        {
            var entities = BaseRepository.GetAll(asNoTracking);
            return entities.To<TDTO>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking = false) => await BaseRepository.GetAllAsync(asNoTracking);

        public virtual async Task<List<TDTO>> GetAllAsync<TDTO>(bool asNoTracking = false)
        {
            var entities = await BaseRepository.GetAllAsync(asNoTracking);
            return Mapper.Map<List<TDTO>>(entities);
        }

        public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => BaseRepository.GetAllByCondition(filterExpression, asNoTracking);

        public virtual IQueryable<TDTO> GetAllByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var entities = BaseRepository.GetAllByCondition(filterExpression, asNoTracking);
            return entities.To<TDTO>();
        }

        public virtual async Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => await BaseRepository.GetAllByConditionAsync(filterExpression, asNoTracking);

        public virtual async Task<List<TDTO>> GetAllByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var entities = await BaseRepository.GetAllByConditionAsync(filterExpression, asNoTracking);
            return Mapper.Map<List<TDTO>>(entities);
        }

        public virtual TDTO GetById<TDTO>(string id)
        {
            var retrievedEntity = GetById(id);
            return Mapper.Map<TDTO>(retrievedEntity);
        }

        public virtual async Task<TDTO> GetByIdAsync<TDTO>(string id)
        {
            var retrievedEntity = await GetByIdAsync(id);
            return Mapper.Map<TDTO>(retrievedEntity);
        }

        public virtual TEntity? GetFirstOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => BaseRepository.GetFirstOrDefaultByCondition(filterExpression, asNoTracking);

        public virtual TDTO? GetFirstOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var firstOrDefaultEntity = BaseRepository.GetFirstOrDefaultByCondition(filterExpression, asNoTracking);
            return Mapper.Map<TDTO>(firstOrDefaultEntity);
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => await BaseRepository.GetFirstOrDefaultByConditionAsync(filterExpression, asNoTracking);

        public virtual async Task<TDTO?> GetFirstOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var firstOrDefaultEntity = await BaseRepository.GetFirstOrDefaultByConditionAsync(filterExpression, asNoTracking);
            return Mapper.Map<TDTO>(firstOrDefaultEntity);
        }

        public virtual TEntity? GetSingleOrDefaultByCondition(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => BaseRepository.GetSingleOrDefaultByCondition(filterExpression, asNoTracking);

        public virtual TDTO? GetSingleOrDefaultByCondition<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var singleOrDefaultEntity = BaseRepository.GetSingleOrDefaultByCondition(filterExpression, asNoTracking);
            return Mapper.Map<TDTO>(singleOrDefaultEntity);
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultByConditionAsync(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
            => await BaseRepository.GetSingleOrDefaultByConditionAsync(filterExpression, asNoTracking);

        public virtual async Task<TDTO?> GetSingleOrDefaultByConditionAsync<TDTO>(Expression<Func<TEntity, bool>> filterExpression, bool asNoTracking = false)
        {
            var singleOrDefaultEntity = await BaseRepository.GetSingleOrDefaultByConditionAsync(filterExpression, asNoTracking);
            return Mapper.Map<TDTO>(singleOrDefaultEntity);
        }

        public virtual void Create<TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            var entityToCreate = Mapper.Map<TEntity>(createDTO);
            BaseRepository.DetachLocalEntity(entityToCreate);
            BaseRepository.Add(entityToCreate);
            BaseRepository.SaveChanges();
        }

        public virtual TDTO CreateAndReturn<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            var entityToCreate = Mapper.Map<TEntity>(createDTO);
            BaseRepository.DetachLocalEntity(entityToCreate);
            entityToCreate = BaseRepository.AddAndReturnEntityFromEntry(entityToCreate);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(entityToCreate);
        }

        public virtual async Task CreateAsync<TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            var entityToCreate = Mapper.Map<TEntity>(createDTO);
            BaseRepository.DeleteAndReturnEntityFromEntry(entityToCreate);
            await BaseRepository.AddAsyncAndReturnEntityFromEntry(entityToCreate);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual async Task<TDTO> CreateAndReturnAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            var entityToCreate = Mapper.Map<TEntity>(createDTO);
            BaseRepository.DetachLocalEntity(entityToCreate);
            entityToCreate = await BaseRepository.AddAsyncAndReturnEntityFromEntry(entityToCreate);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(entityToCreate);
        }

        public virtual void CreateMultiple<TCreateDTO>(List<TCreateDTO> createDTOs)
        {
            var entitiesToCreate = Mapper.Map<TEntity[]>(createDTOs.ToArray());
            BaseRepository.AddRange(entitiesToCreate);
            BaseRepository.SaveChanges();
        }

        public virtual async Task CreateMultipleAsync<TCreateDTO>(List<TCreateDTO> createDTOs)
        {
            var entitiesToCreate = Mapper.Map<TEntity[]>(createDTOs.ToArray());
            await BaseRepository.AddRangeAsync(entitiesToCreate);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual void Update<TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            var entityToUpdate = GetById(id);
            BaseRepository.DetachLocalEntity(entityToUpdate);
            Mapper.Map(updateDTO, entityToUpdate);
            BaseRepository.Update(entityToUpdate);
            BaseRepository.SaveChanges();
        }

        public virtual TDTO UpdateAndReturn<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            var entityToUpdate = GetById(id);
            BaseRepository.DetachLocalEntity(entityToUpdate);
            entityToUpdate = BaseRepository.ReattachUpdateAndReturnEntityFromEntry(entityToUpdate);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(entityToUpdate);
        }

        public virtual async Task UpdateAsync<TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            var entityToUpdate = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToUpdate);
            Mapper.Map(updateDTO, entityToUpdate);
            BaseRepository.Update(entityToUpdate);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual async Task<TDTO> UpdateAndReturnAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            var entityToUpdate = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToUpdate);
            Mapper.Map(updateDTO, entityToUpdate);
            entityToUpdate = BaseRepository.ReattachUpdateAndReturnEntityFromEntry(entityToUpdate);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(entityToUpdate);
        }

        public virtual void UpdateMultiple<TUpdateDTO>(List<TUpdateDTO> updateDTOs)
        {
            var entitiesToUpdate = Mapper.Map<TEntity[]>(updateDTOs.ToArray());
            BaseRepository.UpdateRange(entitiesToUpdate);
            BaseRepository.SaveChanges();
        }

        public virtual async Task UpdateMultipleAsync<TUpdateDTO>(List<TUpdateDTO> updateDTOs)
        {
            var entitiesToUpdate = Mapper.Map<TEntity[]>(updateDTOs.ToArray());
            BaseRepository.UpdateRange(entitiesToUpdate);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual void Delete(string id, string currentUserId)
        {
            var entityToDelete = GetById(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            BaseRepository.Delete(entityToDelete);
            BaseRepository.SaveChanges();
        }

        public virtual TDTO DeleteAndReturn<TDTO>(string id, string currentUserId)
        {
            var entityToDelete = GetById(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            var deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(entityToDelete);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(deletedEntity);
        }

        public virtual async Task DeleteAsync(string id, string currentUserId)
        {
            var entityToDelete = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            BaseRepository.Delete(entityToDelete);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual async Task<TDTO> DeleteAndReturnAsync<TDTO>(string id, string currentUserId)
        {
            var entityToDelete = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            var deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(entityToDelete);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(deletedEntity);
        }

        public virtual void DeleteRange<TDeleteDTO>(List<TDeleteDTO> deleteDTOs)
        {
            var entitiesToDelete = Mapper.Map<TEntity[]>(deleteDTOs.ToArray());
            BaseRepository.DeleteRange(entitiesToDelete);
            BaseRepository.SaveChanges();
        }

        public virtual async Task DeleteRangeAsync<TDeleteDTO>(List<TDeleteDTO> deleteDTOs)
        {
            var entitiesToDelete = Mapper.Map<TEntity[]>(deleteDTOs.ToArray());
            BaseRepository.DeleteRange(entitiesToDelete);
            await BaseRepository.SaveChangesAsync();
        }

        public virtual bool Exists(IQueryable<TEntity> entities, TEntity entityToFind) => BaseRepository.Exists(entities, entityToFind);

        public virtual bool Exists<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind)
        {
            var entities = dtosCollection.To<TEntity>();
            var entityToFind = Mapper.Map<TEntity>(dtoToFind);
            return BaseRepository.Exists(entities, entityToFind);
        }

        public virtual async Task<bool> ExistsAsync(IQueryable<TEntity> entities, TEntity entityToFind) 
            => await BaseRepository.ExistsAsync(entities, entityToFind);

        public virtual async Task<bool> ExistsAsync<TDTO>(IQueryable<TDTO> dtosCollection, TDTO dtoToFind)
        {
            var entities = dtosCollection.To<TEntity>();
            var entityToFind = Mapper.Map<TEntity>(dtoToFind);
            return await BaseRepository.ExistsAsync(entities, entityToFind);
        }

        public int GetTotalRecords() => BaseRepository.GetTotalRecords();

        public async Task<int> GetTotalRecordsAsync() => await BaseRepository.GetTotalRecordsAsync();

        public virtual IQueryable<TEntity> BuildQueryFromRawSql(string queryString, params object[] queryParameters)
            => BaseRepository.BuildQueryFromRawSql(queryString, queryParameters);

        public virtual IQueryable<TDTO> BuildQueryFromRawSql<TDTO>(string queryString, params object[] queryParameters)
        {
            var queriedEntities = BaseRepository.BuildQueryFromRawSql(queryString, queryParameters);
            return queriedEntities.To<TDTO>();
        }

        public virtual int ExecuteSqlRawQuery(string queryString, params object[] queryParameters) 
            => BaseRepository.ExecuteSqlRawQuery(queryString, queryParameters);

        public virtual async Task<int> ExecuteSqlRawQueryAsync(string queryString, params object[] queryParameters)
            => await BaseRepository.ExecuteSqlRawQueryAsync(queryString, queryParameters);

        public virtual TEntity GetById(string id)
        {
            var entityById = BaseRepository.GetById(id);

            if (entityById == null)
            {
                throw new KeyNotFoundException(string.Format(GET_ENTITY_BY_ID_KEY_NOT_FOUND_EXCEPTION_MESSAGE, id));
            }

            return entityById;
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            var entityById = await BaseRepository.GetByIdAsync(id);

            if (entityById == null)
            {
                throw new KeyNotFoundException(string.Format(GET_ENTITY_BY_ID_KEY_NOT_FOUND_EXCEPTION_MESSAGE, id));
            }

            return entityById;
        }
    }
}
