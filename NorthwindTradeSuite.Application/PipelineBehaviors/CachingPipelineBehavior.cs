using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NorthwindTradeSuite.Application.Contracts;
using System.Text;
using System.Text.Json;

namespace NorthwindTradeSuite.Application.PipelineBehaviors
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
    {
        private readonly ILogger<CachingPipelineBehavior<TRequest, TResponse>> _logger;

        private readonly IDistributedCache _distributedCache;

        public CachingPipelineBehavior(ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger, IDistributedCache distributedCache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache)
            {
                return await next();
            }

            TResponse response;

            async Task<TResponse> GetResponseAndAddToDistributedCache()
            {
                response = await next();

                if (response != null)
                {
                    var slidingExpiration = request.SlidingExpirationInMinutes == 0 ? 30 : request.SlidingExpirationInMinutes;
                    var absoluteExpiration = request.AbsoluteExpirationInMinutes == 0 ? 60 : request.AbsoluteExpirationInMinutes;

                    var distributedCacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiration))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpiration));

                    byte[] serializedResponseBytes = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));

                    await _distributedCache.SetAsync(request.CacheKey, serializedResponseBytes, distributedCacheEntryOptions, cancellationToken);
                }

                return response;
            }

            byte[]? cachedResponse = await _distributedCache.GetAsync(request.CacheKey, cancellationToken);

            if (cachedResponse != null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse))!;
                _logger.LogInformation($"Fetched from cache with key: {request.CacheKey}");
            }
            else
            {
                response = await GetResponseAndAddToDistributedCache();
                _logger.LogInformation($"Added to cache with key: {request.CacheKey}");
            }

            return response;
        }
    }
}
