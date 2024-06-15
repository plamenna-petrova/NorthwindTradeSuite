using Microsoft.EntityFrameworkCore;
using NorthwindTradeSuite.Domain.Interfaces;

namespace NorthwindTradeSuite.Persistence
{
    internal static class EntityIndexesConfigurator
    {
        internal static void Configure(ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));

            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
            }
        }
    }
}
