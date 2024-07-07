using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Common.Attributes;

namespace NorthwindTradeSuite.Services.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
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