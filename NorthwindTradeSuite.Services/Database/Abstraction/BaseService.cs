using AutoMapper;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;

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
            var collection = await BaseRepository.GetAll(asNoTracking)
                .To<TDTO>()
                .ToListAsync();

            return collection;
        }

        public async Task<TDTO> GetByIdAsync<TDTO>(string id)
        {
            var retrievedEntity = await GetByIdAsync(id);
            var mappedDTOById = Mapper.Map<TDTO>(retrievedEntity);

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

        public Task<TDTO> UpdateAsync<TDTO, TUpdateDTO>(string id, TUpdateDTO updateDTO, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> DeleteAsync<TDTO>(string id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> HardDeleteAsync<TDTO>(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        private async Task<TEntity> GetByIdAsync(string id)
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
