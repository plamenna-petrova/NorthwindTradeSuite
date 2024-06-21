using CsvHelper.Configuration;
using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetSeedingAdapter<TSeedingDTO> : IDatasetSeedingTarget<TSeedingDTO> where TSeedingDTO : class
    {
        private readonly DatasetFileReaderAdaptee<TSeedingDTO> datasetFileReaderAdaptee = new();

        public DatasetSeedingAdapter(string datasetFileName)
        {
            DatasetFileName = datasetFileName;
        }

        public string DatasetFileName { get; set; }

        public List<TSeedingDTO> RetrieveDatasetObjectsForSeeding() => datasetFileReaderAdaptee.ReadDataset(DatasetFileName);
    }
}
