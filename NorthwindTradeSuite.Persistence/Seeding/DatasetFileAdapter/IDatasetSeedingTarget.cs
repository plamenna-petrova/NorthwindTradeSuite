using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public interface IDatasetSeedingTarget<TEntity> where TEntity : IDeletableEntity
    {
        List<TEntity> RetrieveDatasetObjectsForSeeding();
    }
}
