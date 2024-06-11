using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations
{
    public class CustomerEntityTypeConfiguration : BaseEntityTypeConfiguration<Customer, string>
    {
        public override void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
        }
    }
}
