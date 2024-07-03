using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Common.Attributes;

namespace NorthwindTradeSuite.Services.Extensions
{
    public static class DatabaseServicesConfiguration
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .Scan(scan => scan
                    .FromAssemblyOf<ServicesAssemblyMarker>()
                    .AddClasses(classes => classes.WithAttribute(typeof(TransientServiceAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}