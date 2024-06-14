using NorthwindTradeSuite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseDeletableEntity<TKey> : BaseEntity<TKey>, IDeletableEntity where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
