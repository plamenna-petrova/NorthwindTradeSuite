using NorthwindTradeSuite.Application.Contracts;

namespace NorthwindTradeSuite.Application.Abstraction
{
    public abstract record CachedQuery<TResponse> : IQuery<TResponse>, ICacheable
    {
        public virtual string CacheKey => $"{GetType().Name}-{Guid.NewGuid().ToString()[..8]}";
    }
}
