using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Abstraction;
using NorthwindTradeSuite.Services.Database.Base.Contracts;
using NorthwindTradeSuite.Services.Mappers;

namespace NorthwindTradeSuite.Services.Database.Base
{
    public abstract class DeletableEntityService<TEntity> : BaseService<TEntity>, IDeletableEntityService<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        public DeletableEntityService(IDeletableEntityRepository<TEntity> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
            BaseRepository = baseRepository;
        }

        protected new IDeletableEntityRepository<TEntity> BaseRepository { get; }

        public async Task<List<TDTO>> GetAllWithDeletedAsync<TDTO>(bool asNoTracking = false)
        {
            var collectionIncludingDeletedEntities = await BaseRepository.GetAllWithDeletedEntities(asNoTracking).To<TDTO>().ToListAsync();
            return collectionIncludingDeletedEntities;
        }

        public async Task<TDTO> HardDeleteAsync<TDTO>(string id)
        {
            TEntity retrievedEntityById = await GetByIdAsync<TEntity>(id);

            BaseRepository.DetachLocalEntity(retrievedEntityById);
            TEntity hardDeletedEntity = BaseRepository.HardDeleteAndReturnEntityFromEntry(retrievedEntityById);
            await BaseRepository.SaveChangesAsync();

            TDTO mappedDTOToReturn = Mapper.Map<TDTO>(hardDeletedEntity);

            return mappedDTOToReturn;
        }

        public async Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId)
        {
            TEntity retrievedEntityById = await GetByIdAsync<TEntity>(id);

            BaseRepository.DetachLocalEntity(retrievedEntityById);
            TEntity restoredEntity = BaseRepository.RestoreAndReturnEntityFromEntry(retrievedEntityById);
            await BaseRepository.SaveChangesAsync();

            TDTO mappedDTOToReturn = Mapper.Map<TDTO>(restoredEntity);

            return mappedDTOToReturn;
        }
    }
}
