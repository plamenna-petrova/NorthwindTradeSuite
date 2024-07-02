using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Application.Contracts;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQueryable<TResponse>, ICacheable
    {
        private IMemoryCache _memoryCache = null!;

        private ILogger<CachingPipelineBehavior<TRequest, TResponse>> _logger = null!;

        public CachingPipelineBehavior(IMemoryCache memoryCache, ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var cachedResult = _memoryCache.Get<TResponse>(request.Key);

            if (cachedResult != null) 
            {
                _logger.LogInformation($"Cache hit for {requestName}");
                return cachedResult;
            }

            _logger.LogInformation($"Cache miss for {requestName}");

            var data = await next();

            _memoryCache.Set(request.Key, data, request.Expiration);

            return data;
        }
    }
}
