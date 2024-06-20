using CsvHelper.Configuration;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetSeedingAdapter<TEntity, TMap> : IDatasetSeedingTarget<TEntity> 
        where TEntity : class, new()
        where TMap : ClassMap<TEntity>
    {
        private readonly DatasetFileReaderAdaptee<TEntity, TMap> datasetFileReaderAdaptee = new();

        public DatasetSeedingAdapter(string datasetFileName)
        {
            DatasetFileName = datasetFileName;
        }

        public string DatasetFileName { get; set; }

        public List<TEntity> RetrieveDatasetObjectsForSeeding() => datasetFileReaderAdaptee.ReadDataset(DatasetFileName);
    }
}
