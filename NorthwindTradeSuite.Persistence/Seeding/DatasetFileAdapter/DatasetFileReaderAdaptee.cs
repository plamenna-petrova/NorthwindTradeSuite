using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Reflection;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DirectoriesAndFileLocationsConstants;
using static NorthwindTradeSuite.Common.GlobalConstants.ExceptionMessages;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetFileReaderAdaptee<TSeedingDTO> where TSeedingDTO : class
    {
        private static readonly Dictionary<DatasetFileType, string> datasetsFolders = new()
        {
            { DatasetFileType.CSV, CSV_FILES_FOLDER },
            { DatasetFileType.JSON, JSON_FILES_FOLDER }
        };

        public IQueryable<TSeedingDTO> ReadDataset(string datasetFileName)
        {
            List<TSeedingDTO> readDatasetObjects = null!;

            var (datasetFilePath, datasetFileType) = GetDatasetFilePathAndType(datasetFileName);

            if (datasetFilePath != null && datasetFileType != null)
            {
                switch (datasetFileType)
                {
                    case DatasetFileType.CSV:
                        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = true,
                        };

                        using (var csvStreamReader = new StreamReader(datasetFilePath))
                        using (var csvReader = new CsvReader(csvStreamReader, csvConfiguration))
                        {
                            readDatasetObjects = csvReader.GetRecords<TSeedingDTO>().ToList();
                        }
                        break;
                    case DatasetFileType.JSON:
                        string jsonFileContentForSeeding = File.ReadAllText(datasetFilePath);
                        readDatasetObjects = JsonConvert.DeserializeObject<List<TSeedingDTO>>(jsonFileContentForSeeding)!;
                        break;
                }
            }

            return readDatasetObjects.AsQueryable();
        }

        private (string datasetFilePath, DatasetFileType? datasetFileType) GetDatasetFilePathAndType(string datasetFileName)
        {
            string solutionsDirectory = GetSolutionDirectory();
            string datasetFileExtension = GetDatasetFileExtension(datasetFileName);

            if (!Enum.TryParse(datasetFileExtension, true, out DatasetFileType parsedDatasetFileType))
            {
                throw new NotSupportedException(string.Format(UNSUPPORTED_DATASET_FILE_TYPE_NOT_SUPPORTED_EXCEPTION_MESSAGE, datasetFileExtension));
            }

            string formattedDatasetsSubfolderPathInAssembly = string.Format(DATASETS_DIRECTORY_RELATIVE_PATH, datasetsFolders[parsedDatasetFileType]);
            string datasetsDirectoryFullPath = Path.Combine(solutionsDirectory, formattedDatasetsSubfolderPathInAssembly);
            string[] datasetsFilesForSeeding = Directory.GetFiles(datasetsDirectoryFullPath, $"*.{datasetFileExtension}", SearchOption.AllDirectories);

            if (!datasetsFilesForSeeding.Any())
            {
                return (null!, null!);
            }

            var targetDatasetFilePath = datasetsFilesForSeeding.SingleOrDefault(dfs => dfs.Split("\\").Last().Equals(datasetFileName));

            if (targetDatasetFilePath == null)
            {
                return (null!, null!);
            }

            return (targetDatasetFilePath!, parsedDatasetFileType);
        }

        private string GetSolutionDirectory()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            DirectoryInfo solutionTargetDirectoryInfo = Directory.GetParent(executingAssembly.Location)!;
            string solutionTargetDirectoryFullName = solutionTargetDirectoryInfo.FullName;

            while (!Directory.GetFiles(solutionTargetDirectoryFullName, "*.sln").Any())
            {
                DirectoryInfo solutionParentDirectoryInfo = Directory.GetParent(solutionTargetDirectoryFullName)!;
                solutionTargetDirectoryFullName = solutionParentDirectoryInfo!.FullName;

                if (solutionTargetDirectoryFullName == null)
                {
                    throw new Exception(SOLUTION_DIRECTORY_NOT_FOUND_EXCEPTION_MESSAGE);
                }
            }

            return solutionTargetDirectoryFullName;
        }

        private string GetDatasetFileExtension(string datasetFileName) => datasetFileName.Split(".").Last();
    }
}
