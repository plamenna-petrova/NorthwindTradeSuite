using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseEntity : IBaseEntity<string>, IAuditInfo
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString()[..8];        
        }

        public string Id { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedBy { get; set; } = null!;
    }
}
