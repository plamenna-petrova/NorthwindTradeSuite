﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Mapping.AutoMapper;
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

        public override IQueryable<TEntity> GetAll(bool asNoTracking = false) => BaseRepository.GetAll(asNoTracking);

        public override void Delete(string id)
        {
            var entityToDelete = base.GetById(id);
            BaseRepository.Delete(entityToDelete);
            BaseRepository.SaveChanges();
        }

        public override TDTO DeleteAndReturn<TDTO>(string id)
        {
            var entityToDelete = base.GetById(id);
            TEntity deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(entityToDelete);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(deletedEntity);
        }

        public override async Task DeleteAsync(string id)
        {
            var entityToDelete = await base.GetByIdAsync(id);
            BaseRepository.Delete(entityToDelete);
            await BaseRepository.SaveChangesAsync();
        }

        public override async Task<TDTO> DeleteAndReturnAsync<TDTO>(string id)
        {
            var entityToDelete = await base.GetByIdAsync(id);
            TEntity deletedEntity = BaseRepository.DeleteAndReturnEntityFromEntry(entityToDelete);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(deletedEntity);
        }

        public IQueryable<TEntity> GetAllWithDeleted(bool asNoTracking = false) => BaseRepository.GetAllWithDeletedEntities(asNoTracking);

        public IQueryable<TDTO> GetAllWithDeleted<TDTO>(bool asNoTracking = false) => BaseRepository.GetAllWithDeletedEntities(asNoTracking).To<TDTO>();

        public async Task<List<TEntity>> GetAllWithDeletedAsync(bool asNoTracking = false)
            => await BaseRepository.GetAllWithDeletedEntities(asNoTracking).ToListAsync();

        public async Task<List<TDTO>> GetAllWithDeletedAsync<TDTO>(bool asNoTracking = false)
            => await BaseRepository.GetAllWithDeletedEntities(asNoTracking).To<TDTO>().ToListAsync();

        public async Task<List<TEntity>> GetAllWithOptionalDeletionFlagAsync(bool isDeletedFlag = false, bool asNoTracking = false)
            => await BaseRepository.GetAllWithOptionalDeletionFlagAsync(isDeletedFlag, asNoTracking);

        public async Task<List<TDTO>> GetAllWithOptionalDeletionFlagAsync<TDTO>(bool isDeletedFlag = false, bool asNoTracking = false)
        {
            var deletableEntities = await BaseRepository.GetAllWithOptionalDeletionFlagAsync(isDeletedFlag, asNoTracking);
            return Mapper.Map<List<TDTO>>(deletableEntities);
        }

        public IQueryable<TEntity> GetByIdWithOptionalDeletionFlagAsQueryable(string id, bool isDeletedFlag, bool asNoTracking = false)
            => BaseRepository.GetByIdWithOptionalDeletionFlagAsQueryable(id, isDeletedFlag, asNoTracking);

        public IQueryable<TDTO> GetByIdWithOptionalDeletionFlagAsQueryable<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false)
            => BaseRepository.GetByIdWithOptionalDeletionFlagAsQueryable(id, isDeletedFlag, asNoTracking).To<TDTO>();

        public async Task<TEntity?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false)
            => await BaseRepository.GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(id, isDeletedFlag, asNoTracking);

        public async Task<TDTO?> GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false)
        {
            var firstOrDefaultDeletableEntity = await BaseRepository.GetFirstOrDefaultByIdWithOptionalDeletionFlagAsync(id, isDeletedFlag, asNoTracking);
            return Mapper.Map<TDTO>(firstOrDefaultDeletableEntity);
        }

        public async Task<TEntity?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(string id, bool isDeletedFlag, bool asNoTracking = false)
            => await BaseRepository.GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(id, isDeletedFlag, asNoTracking);

        public async Task<TDTO?> GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync<TDTO>(string id, bool isDeletedFlag, bool asNoTracking = false)
        {
            var singleOrDefaultDeletableEntity = await BaseRepository.GetSingleOrDefaultByIdWithOptionalDeletionFlagAsync(id, isDeletedFlag, asNoTracking);
            return Mapper.Map<TDTO>(singleOrDefaultDeletableEntity);
        }

        public void HardDelete(string id)
        {
            var entityToHardDelete = base.GetById(id);
            BaseRepository.DetachLocalEntity(entityToHardDelete);
            BaseRepository.HardDelete(entityToHardDelete);
            BaseRepository.SaveChanges();
        }

        public TDTO HardDelete<TDTO>(string id)
        {
            var entityToHardDelete = base.GetById(id);
            BaseRepository.DetachLocalEntity(entityToHardDelete);
            var hardDeletedEntity = BaseRepository.HardDeleteAndReturnEntityFromEntry(entityToHardDelete);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(hardDeletedEntity);
        }

        public async Task HardDeleteAsync(string id)
        {
            var entityToHardDelete = await base.GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToHardDelete);
            BaseRepository.HardDelete(entityToHardDelete);
            await BaseRepository.SaveChangesAsync();
        }

        public async Task<TDTO> HardDeleteAsync<TDTO>(string id)
        {
            var entityToHardDelete = await base.GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToHardDelete);
            var hardDeletedEntity = BaseRepository.HardDeleteAndReturnEntityFromEntry(entityToHardDelete);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(hardDeletedEntity);
        }

        public void Restore(string id)
        {
            var entityToRestore = base.GetById(id);
            BaseRepository.DetachLocalEntity(entityToRestore);
            BaseRepository.Restore(entityToRestore);
            BaseRepository.SaveChanges();
        }

        public TDTO Restore<TDTO>(string id)
        {
            var entityToRestore = base.GetById(id);
            BaseRepository.DetachLocalEntity(entityToRestore);
            var restoredEntity = BaseRepository.RestoreAndReturnEntityFromEntry(entityToRestore);
            BaseRepository.SaveChanges();
            return Mapper.Map<TDTO>(restoredEntity);
        }

        public async Task RestoreAsync(string id)
        {
            var entityToRestore = await base.GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToRestore);
            BaseRepository.Restore(entityToRestore);
            await BaseRepository.SaveChangesAsync();
        }

        public async Task<TDTO> RestoreAsync<TDTO>(string id)
        {
            var entityToRestore = await base.GetByIdAsync(id);
            BaseRepository.DetachLocalEntity(entityToRestore);
            var restoredEntity = BaseRepository.RestoreAndReturnEntityFromEntry(entityToRestore);
            await BaseRepository.SaveChangesAsync();
            return Mapper.Map<TDTO>(restoredEntity);
        }
    }
}
