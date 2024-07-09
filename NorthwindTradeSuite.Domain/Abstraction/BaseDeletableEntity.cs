using NorthwindTradeSuite.Domain.Contracts;

namespace NorthwindTradeSuite.Domain.Abstraction
{
    public abstract class BaseDeletableEntity : BaseEntity, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
