using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DirectoriesAndFileLocationsConstants;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetFileReaderAdaptee<TSeedingDTO> where TSeedingDTO : class
    {
        public List<TSeedingDTO> ReadDataset(string datasetFileName)
        {
            List<TSeedingDTO> readDatasetObjects = null!;

            string datasetFileExtension = GetDatasetFileExtension(datasetFileName);

            switch (datasetFileExtension.ToUpper())
            {
                case nameof(DatasetFileType.CSV):
                    var csvDatasetFilePath = GetDatasetFilePath(datasetFileName, CSV_FILES_FOLDER);

                    if (csvDatasetFilePath != null)
                    {
                        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = true,
                        };

                        using var csvStreamReader = new StreamReader(csvDatasetFilePath);
                        using var csvReader = new CsvReader(csvStreamReader, csvConfiguration);
                        readDatasetObjects = csvReader.GetRecords<TSeedingDTO>().ToList();
                    }
                    break;
                case nameof(DatasetFileType.JSON):
                    var jsonDatasetFilePath = GetDatasetFilePath(datasetFileName, JSON_FILES_FOLDER);

                    if (jsonDatasetFilePath != null)
                    {
                        string targetJSONFileContent = File.ReadAllText(jsonDatasetFilePath);
                    }
                    break;
            }

            return readDatasetObjects;
        }

        private string GetDatasetFilePath(string datasetFileName, string datasetsFilesFolder)
        {
            string solutionsDirectory = GetSolutionDirectory();
            string datasetsDirectoryPath = Path.Combine(solutionsDirectory, string.Format(DATASETS_DIRECTORY_RELATIVE_PATH, datasetsFilesFolder));
            string datasetFileExtension = GetDatasetFileExtension(datasetFileName);
            string[] datasetsFilesForSeeding = Directory.GetFiles(datasetsDirectoryPath, $"*.{datasetFileExtension}", SearchOption.AllDirectories);

            if (!datasetsFilesForSeeding.Any())
            {
                return null!;
            }

            var targetDatasetFilePath = datasetsFilesForSeeding.SingleOrDefault(csvFile => csvFile.EndsWith($"{datasetFileName}"));

            return targetDatasetFilePath!;
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
                    throw new Exception("Solution directory not found.");
                }
            }

            return solutionTargetDirectoryFullName;
        }

        private string GetDatasetFileExtension(string datasetFileName) => datasetFileName.Split(".").Last();
    }
}
