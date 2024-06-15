using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Persistence.Seeders.Contracts;

namespace NorthwindTradeSuite.Persistence.Seeding.Abstraction
{
    public abstract class BaseSeeder : ISeeder
    {
        public BaseSeeder(IServiceProvider serviceProvider, ILogger logger, string datasetFileName)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
            DatasetFileName = datasetFileName;
        }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected ILogger Logger { get; private set; }

        protected string DatasetFileName { get; private set; }

        public abstract Task SeedAsync();
    }
}
