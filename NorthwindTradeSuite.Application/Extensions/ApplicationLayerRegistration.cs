﻿using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using NorthwindTradeSuite.Application.PipelineBehaviors;
using NorthwindTradeSuite.Application.Features.Accounts.Commands.Register;
using MediatR;
using System.Reflection;

namespace NorthwindTradeSuite.Application.Extensions
{
    public static class ApplicationLayerRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddValidatorsFromAssembly(typeof(ApplicationLayerRegistration).Assembly);

            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ApplicationLayerRegistration).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingPipelineBehavior<,>));
            });

            serviceCollection.AddMemoryCache();

            return serviceCollection;
        }
    }
}
