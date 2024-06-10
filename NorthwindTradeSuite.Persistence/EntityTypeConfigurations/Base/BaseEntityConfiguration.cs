using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Interfaces;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base
{
    public class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.Id);

            entityTypeBuilder.Property(e => e.CreatedAt)
                             .IsRequired()
                             .ValueGeneratedOnAdd()
                             .Metadata
                             .SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            entityTypeBuilder.Property(e => e.ModifiedAt)
                             .IsRequired(false);

            entityTypeBuilder.HasCheckConstraint(
               string.Format(string.Empty, string.Empty),
               $"{nameof(IAuditInfo.ModifiedAt)} >= {nameof(IAuditInfo.CreatedAt)}"
           );
        }
    }
}
