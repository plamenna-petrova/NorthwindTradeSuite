using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetSeedingAdapter<TEntity> : IDatasetSeedingTarget<TEntity> where TEntity : IDeletableEntity
    {
        private readonly DatasetFileReaderAdaptee<TEntity> datasetFileReaderAdaptee = new();

        public DatasetSeedingAdapter(string datasetFileName)
        {
            DatasetFileName = datasetFileName;
        }

        public string DatasetFileName { get; set; }

        public List<TEntity> RetrieveDatasetObjectsForSeeding() => datasetFileReaderAdaptee.ReadDataset(DatasetFileName);
    }
}
