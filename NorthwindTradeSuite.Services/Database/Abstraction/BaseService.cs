using AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Services.Database.Base.Contracts;

namespace NorthwindTradeSuite.Services.Database.Abstraction
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            BaseRepository = baseRepository;
            Mapper = mapper;
        }

        protected virtual IBaseRepository<TEntity> BaseRepository { get; set; }

        protected virtual IMapper Mapper { get; }

        public IQueryable<TEntity> GetAll(bool asNoTracking = false)
        {
            var collection = BaseRepository.GetAll(asNoTracking);
            return collection;
        }

        public async Task<List<TDTO>> GetAllAsync<TDTO>(bool asNoTracking = false)
        {
            var collection = await BaseRepository.GetAll(asNoTracking).To<TDTO>().ToListAsync();
            return collection;
        }

        public async Task<TDTO> GetByIdAsync<TDTO>(string id)
        {
            TEntity retrievedEntityById = await GetByIdAsync<TEntity>(id);
            TDTO mappedDTOById = Mapper.Map<TDTO>(retrievedEntityById);
            return mappedDTOById;
        }

        public async Task<TDTO> CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            TEntity entityToCreate = Mapper.Map<TEntity>(createDTO);

            BaseRepository.DetachLocalEntity(entityToCreate);
            entityToCreate = await BaseRepository.AddAsyncAndReturnEntityFromEntry(entityToCreate);
            await BaseRepository.SaveChangesAsync();

            TDTO mappedDTOToReturn = Mapper.Map<TDTO>(entityToCreate);

            return mappedDTOToReturn;
        }

        public async Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            TEntity retrievedEntityById = await GetByIdAsync<TEntity>(id);

            BaseRepository.DetachLocalEntity(retrievedEntityById);

            TEntity entityToUpdate = Mapper.Map(updateDTO, retrievedEntityById);
            entityToUpdate = BaseRepository.ReattachUpdateAndReturnEntityFromEntry(entityToUpdate);
            await BaseRepository.SaveChangesAsync();

            TDTO mappedDTOToReturn = Mapper.Map<TDTO>(entityToUpdate);

            return mappedDTOToReturn;
        }

        public async Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId)
        {
            TEntity retrievedEntityById = await GetByIdAsync<TEntity>(id);

            BaseRepository.DetachLocalEntity(retrievedEntityById);
            TEntity deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(retrievedEntityById);
            await BaseRepository.SaveChangesAsync();

            TDTO mappedDTOToReturn = Mapper.Map<TDTO>(deletedEntity);

            return mappedDTOToReturn;
        }

        protected async Task<TEntity> GetByIdAsync(string id)
        {
            var entityById = await BaseRepository.GetByIdAsync(id);

            if (entityById == null)
            {
                throw new KeyNotFoundException($"Entity with ID '{id}' not found");
            }

            return entityById;
        }
    }
}
