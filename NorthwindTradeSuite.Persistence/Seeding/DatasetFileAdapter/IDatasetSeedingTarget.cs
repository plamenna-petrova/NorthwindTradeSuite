using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public interface IDatasetSeedingTarget<TEntity> where TEntity : class, new()
    {
        List<TEntity> RetrieveDatasetObjectsForSeeding();
    }
}
