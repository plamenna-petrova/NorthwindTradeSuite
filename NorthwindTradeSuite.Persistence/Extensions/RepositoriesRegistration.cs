using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Persistence.Repositories.Contracts;
using NorthwindTradeSuite.Persistence.Repositories.Implementation;

namespace NorthwindTradeSuite.Persistence.Extensions
{
    public static class RepositoriesRegistration
    {
        public static IServiceCollection AddPersistenceLayerServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IDeletableEntityRepository<>), typeof(DeletableEntityRepository<>));

            return serviceCollection;
        }
    }
}
