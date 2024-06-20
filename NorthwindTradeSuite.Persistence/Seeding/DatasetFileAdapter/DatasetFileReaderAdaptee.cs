using CsvHelper;
using CsvHelper.Configuration;
using NorthwindTradeSuite.Domain.Contracts;
using System.Globalization;
using System.Reflection;
using static NorthwindTradeSuite.Common.GlobalConstants.Seeding.DirectoriesAndFileLocationsConstants;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public class DatasetFileReaderAdaptee<TEntity> where TEntity : IDeletableEntity
    {
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

            return readDatasetObjects;
        }

        private string GetJSONContent(string jsonFileName)
        {
            string solutionsDirectory = GetSolutionDirectory();
            string jsonDatasetsDirectoryPath = Path.Combine(solutionsDirectory, string.Format(DATASETS_DIRECTORY_RELATIVE_PATH, JSON_FILES_FOLDER));
            string[] jsonDatasetsFilesForSeeding = Directory.GetFiles(jsonDatasetsDirectoryPath, "*.json", SearchOption.AllDirectories);

            if (!jsonDatasetsFilesForSeeding.Any())
            {
                return null!;
            }

            string targetJSONFileForSeeding = jsonFileName + ".json";
            var targetJSONFilePath = jsonDatasetsFilesForSeeding.SingleOrDefault(jf => jf.EndsWith(targetJSONFileForSeeding));

            if (targetJSONFilePath == null)
            {
                return null!;
            }

            string targetJSONFileContent = File.ReadAllText(targetJSONFilePath);

            return targetJSONFileContent;
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
    }
}
