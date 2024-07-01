using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Services.Database.Abstraction;
using NorthwindTradeSuite.Services.Database.Base.Contracts;

namespace NorthwindTradeSuite.Services.Database.Base
{
    public abstract class DeletableEntityService<TEntity> : BaseService<TEntity>, IDeletableEntityService<TEntity>
        where TEntity : BaseDeletableEntity<string>
    {
        protected readonly new IDeletableEntityRepository<TEntity> BaseRepository;

        public DeletableEntityService(IDeletableEntityRepository<TEntity> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
            BaseRepository = baseRepository;
        }

        public async Task<List<TDTO>> GetAllWithDeletedAsync<TDTO>(bool asNoTracking = false)
        {
            var entities = await BaseRepository.GetAllWithDeletedEntities(asNoTracking).ToListAsync();
            return Mapper.Map<List<TDTO>>(entities);
        }

        public async Task<TDTO> HardDeleteAsync<TDTO>(string id)
        {
            var entityToDelete = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToDelete);
            var hardDeletedEntity = BaseRepository.HardDeleteAndReturnEntityFromEntry(entityToDelete);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(hardDeletedEntity);
        }

        public async Task<TDTO> RestoreAsync<TDTO>(string id, string currentUserId)
        {
            var entityToRestore = await GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToRestore);
            var restoredEntity = BaseRepository.RestoreAndReturnEntityFromEntry(entityToRestore);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(restoredEntity);
        }
    }
}
