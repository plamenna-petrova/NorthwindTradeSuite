using CsvHelper;
using CsvHelper.Configuration;
using NorthwindTradeSuite.Domain.Interfaces;
using System.Globalization;
using System.Reflection;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetFileReaderAdaptee<TEntity> where TEntity : IDeletableEntity
    {
        private const string DATASETS_DIRECTORY_RELATIVE_PATH = @"NorthwindTradeSuite.Persistence\Seeding\Datasets\{0}";

        private const string CSV_FILES_FOLDER = "CSVFiles";

        private const string JSON_FILES_FOLDER = "JSONFiles";

        public List<TEntity> ReadDataset(string datasetFileName)
        {
            List<TEntity> readDatasetObjects = null!;

            //switch (dataSetFileType)
            //{
            //    case DatasetFileType.CSV:
            //        using (var csvStreamReader = new StreamReader("path\\to\\file.csv"))
            //        using (var csvReader = new CsvReader(csvStreamReader, CultureInfo.InvariantCulture))
            //        { 
            //            readDatasetObjects = csvReader.GetRecords<TEntity>().ToList();
            //        }
            //        break;
            //    case DatasetFileType.JSON:
            //        break;
            //}

            var test = GetJSONContent(datasetFileName);

            return readDatasetObjects;
        }

        private string GetJSONContent(string jsonFileName)
        {
            string jsonDatasetsDirectoryPath = Path.Combine(GetSolutionDirectory(), string.Format(DATASETS_DIRECTORY_RELATIVE_PATH, JSON_FILES_FOLDER));
            string[] jsonDatasetsFilesForSeeding = Directory.GetFiles(jsonDatasetsDirectoryPath, "*.json", SearchOption.AllDirectories);

            if (!jsonDatasetsFilesForSeeding.Any())
            {
                return null!;
            }

            string jsonFile = jsonFileName + ".json";
            var jsonPath = jsonDatasetsFilesForSeeding.SingleOrDefault(jf => jf.EndsWith(jsonFile));

            if (jsonPath == null)
            {
                return null!;
            }

            string jsonContent = File.ReadAllText(jsonPath);

            return jsonContent;
        }

        private string GetSolutionDirectory()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            DirectoryInfo solutionDirectoryInfo = Directory.GetParent(executingAssembly.Location)!;
            string solutionDirectoryFullName = solutionDirectoryInfo.FullName;

            while (!Directory.GetFiles(solutionDirectoryFullName, "*.sln").Any())
            {
                DirectoryInfo solutionParentDirectoryInfo = Directory.GetParent(solutionDirectoryFullName)!;
                solutionDirectoryFullName = solutionParentDirectoryInfo!.FullName;

                if (solutionDirectoryFullName == null)
                {
                    throw new Exception("Solution directory not found.");
                }
            }

            return solutionDirectoryFullName;
        }
    }
}
