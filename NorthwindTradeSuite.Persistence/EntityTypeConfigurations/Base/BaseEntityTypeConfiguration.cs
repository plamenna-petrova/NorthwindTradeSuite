using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Contracts;
using static NorthwindTradeSuite.Common.GlobalConstants.SQLConstants;

namespace NorthwindTradeSuite.Persistence.EntityTypeConfigurations.Base
{
    public class BaseEntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseDeletableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => e.Id);

            entityTypeBuilder
                .Property(e => e.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .Metadata
                .SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            entityTypeBuilder
                .Property(e => e.ModifiedAt)
                .IsRequired(false);

            entityTypeBuilder
                .Property(e => e.CreatedBy)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.ModifiedBy)
                .IsRequired(false);

            entityTypeBuilder
                .Property(e => e.DeletedBy)
                .IsRequired(false);

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, 
                GetCheckConstraintTableColumn(typeof(TEntity).Name, nameof(IAuditInfo.ModifiedAt))),
                $"{nameof(IAuditInfo.ModifiedAt)} >= {nameof(IAuditInfo.CreatedAt)}"
            );

            entityTypeBuilder.HasCheckConstraint(
                string.Format(CHECK_CONSTRAINT_TEMPLATE, 
                GetCheckConstraintTableColumn(typeof(TEntity).Name, nameof(IDeletableEntity.DeletedAt))),
                $"{nameof(IDeletableEntity.DeletedAt)} >= {nameof(IAuditInfo.ModifiedAt)}"
            );
        }

        protected virtual string GetCheckConstraintTableColumn(params object[] checkConstraintTokens) =>
            string.Join("_", checkConstraintTokens);
    }
}
