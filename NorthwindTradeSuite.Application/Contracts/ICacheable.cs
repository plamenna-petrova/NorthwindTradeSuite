using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Application.Contracts
{
    public interface ICacheable
    {
        public string Key { get; }

        public DateTimeOffset Expiration { get; }
    }
}