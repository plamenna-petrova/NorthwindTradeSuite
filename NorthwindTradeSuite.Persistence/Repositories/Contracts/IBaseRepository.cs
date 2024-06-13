using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Persistence.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsNoTracking();

        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filterExpression);

        TEntity GetById(string id);

        void Add(TEntity entityToAdd);

        void AddRange(TEntity[] entitiesRangeToAdd);

        void Update(TEntity entityToUpdate);

        void UpdateRange(TEntity[] entitiesRangeToUpdate);

        void Delete(TEntity entityToDelete);

        void DeleteRange(TEntity[] entitiesRangeToDelete);

        bool Exists(IQueryable<TEntity> entities, TEntity entityToFind);
    }
}
