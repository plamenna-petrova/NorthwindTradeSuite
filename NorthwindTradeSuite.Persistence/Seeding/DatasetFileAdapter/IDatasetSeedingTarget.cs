using Microsoft.Extensions.Logging;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public interface IDatasetSeedingTarget<TSeedingDTO> where TSeedingDTO : class
    {
        IQueryable<TSeedingDTO> RetrieveDatasetObjectsForSeeding(ILogger logger);
    }
}
