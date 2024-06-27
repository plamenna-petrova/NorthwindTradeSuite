using AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Services.Database.Base.Contracts;

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

        public IQueryable<TEntity> GetAll(bool asNoTracking = false)
        {
            return BaseRepository.GetAll(asNoTracking);
        }

        public async Task<List<TDTO>> GetAllAsync<TDTO>(bool asNoTracking = false)
        {
            var entities = await BaseRepository.GetAll(asNoTracking).ToListAsync();
            return Mapper.Map<List<TDTO>>(entities);
        }

        public async Task<TDTO> GetByIdAsync<TDTO>(string id)
        {
            var retrievedEntity = await GetEntityByIdAsync(id);
            return Mapper.Map<TDTO>(retrievedEntity);
        }

        public async Task<TDTO> CreateAsync<TDTO, TCreateDTO>(TCreateDTO createDTO, string currentUserId)
        {
            var entityToCreate = Mapper.Map<TEntity>(createDTO);
            BaseRepository.DetachLocalEntity(entityToCreate);
            entityToCreate = await BaseRepository.AddAsyncAndReturnEntityFromEntry(entityToCreate);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(entityToCreate);
        }

        public async Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            var entityToUpdate = await GetEntityByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToUpdate);
            Mapper.Map(updateDTO, entityToUpdate);
            entityToUpdate = BaseRepository.ReattachUpdateAndReturnEntityFromEntry(entityToUpdate);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(entityToUpdate);
        }

        public async Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId)
        {
            var entityToDelete = await GetEntityByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            var deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(entityToDelete);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(deletedEntity);
        }

        protected async Task<TEntity> GetEntityByIdAsync(string id)
        {
            var entity = await BaseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID '{id}' not found");
            }

            return entity;
        }
    }
}
