using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Common.Attributes;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Services.Database.Base.Contracts;

namespace NorthwindTradeSuite.Infrastructure.Extensions
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .Scan(scan => scan
                    .FromAssemblyOf<IBaseService<BaseEntity<string>>>()
                    .AddClasses(classes => classes.WithAttribute(typeof(TransientServiceAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
