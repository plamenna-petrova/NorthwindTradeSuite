using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseDeletableEntity<TKey> : BaseEntity<TKey>, IDeletableEntity where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
