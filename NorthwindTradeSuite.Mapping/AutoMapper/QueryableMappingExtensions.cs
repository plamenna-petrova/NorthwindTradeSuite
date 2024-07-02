using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;

namespace NorthwindTradeSuite.Mapping.AutoMapper
{
    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable queryableSource, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (queryableSource == null)
            {
                throw new ArgumentNullException(nameof(queryableSource));
            }

            return queryableSource.ProjectTo(AutoMapperConfigurator.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(this IQueryable queryableSource, object parameters)
        {
            if (queryableSource == null)
            {
                throw new ArgumentNullException(nameof(queryableSource));
            }

            return queryableSource.ProjectTo<TDestination>(AutoMapperConfigurator.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}
