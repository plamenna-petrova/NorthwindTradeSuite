using NorthwindTradeSuite.Domain.Abstraction;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class Category : BaseEntity<string>
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] Picture { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
