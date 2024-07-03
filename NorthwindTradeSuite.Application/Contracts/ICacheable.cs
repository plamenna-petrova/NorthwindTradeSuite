using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface ICacheable
    {
        bool BypassCache { get; }

        string CacheKey { get; }

        int SlidingExpirationInMinutes { get; }

        int AbsoluteExpirationInMinutes { get; }
    }
}