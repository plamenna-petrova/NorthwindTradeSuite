using NorthwindTradeSuite.Domain.Interfaces;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, IAuditInfo where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
