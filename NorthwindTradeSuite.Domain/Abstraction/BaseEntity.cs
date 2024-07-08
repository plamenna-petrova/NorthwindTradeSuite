using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, IAuditInfo where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }

        public string ModifiedBy { get; set; } = null!;
    }
}
