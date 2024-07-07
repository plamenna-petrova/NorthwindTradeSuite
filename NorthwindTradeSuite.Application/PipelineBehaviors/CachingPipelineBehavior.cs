using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Application.Contracts;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IMemoryCache _memoryCache;

        private readonly ILogger<CachingPipelineBehavior<TRequest, TResponse>> _logger;

        public CachingPipelineBehavior(IMemoryCache memoryCache, ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;

            _logger.LogInformation($"{requestName} is configured for caching.");

            if (_memoryCache.TryGetValue(request.CacheKey, out TResponse? response))
            {
                _logger.LogInformation($"Returning cached value for {requestName}");
                return response!;
            }

            _logger.LogInformation($"{requestName} Cache Key: {request.CacheKey} is not inside the cache, executing request.");
            response = await next();
            _memoryCache.Set(request.CacheKey, response, TimeSpan.FromSeconds(60));

            return response;
        }
    }
}
