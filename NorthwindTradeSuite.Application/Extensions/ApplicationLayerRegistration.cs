using Microsoft.Extensions.DependencyInjection;
using NorthwindTradeSuite.Application.PipelineBehaviors;

namespace NorthwindTradeSuite.Application.Extensions
{
    public static class ApplicationLayerRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ApplicationLayerRegistration).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingPipelineBehavior<,>));
            });

            serviceCollection.AddDistributedMemoryCache();

            return serviceCollection;
        }
    }
}
