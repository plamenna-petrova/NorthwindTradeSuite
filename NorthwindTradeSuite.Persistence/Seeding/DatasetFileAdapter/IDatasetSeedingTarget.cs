using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Persistence.Seeding.DatasetFileAdapter
{
    public interface IDatasetSeedingTarget<TEntity> where TEntity : IDeletableEntity
    {
        List<TEntity> RetrieveDatasetObjectsForSeeding();
    }
}
