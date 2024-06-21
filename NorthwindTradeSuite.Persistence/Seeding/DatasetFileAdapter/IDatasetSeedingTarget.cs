using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public interface IDatasetSeedingTarget<TSeedingDTO> where TSeedingDTO : class
    {
        List<TSeedingDTO> RetrieveDatasetObjectsForSeeding();
    }
}
