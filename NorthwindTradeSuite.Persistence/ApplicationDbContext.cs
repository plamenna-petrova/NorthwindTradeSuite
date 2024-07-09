using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Contracts;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.Identity;
using System.Reflection;
using static NorthwindTradeSuite.Common.GlobalConstants.ConnectionConstants;

namespace NorthwindTradeSuite.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, 
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private IConfigurationBuilder _configurationBuilder = null!;

        private IConfigurationRoot _configurationRoot = null!;

        private static readonly MethodInfo SetIsDeletedQueryFilterMethodInfo =
            typeof(ApplicationDbContext).GetMethod(nameof(SetIsDeletedQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)!;

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public virtual DbSet<Category> Categories { get; set; } = null!;

        public virtual DbSet<Customer> Customers { get; set; } = null!;

        public virtual DbSet<Employee> Employees { get; set; } = null!;

        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; } = null!;

        public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;

        public virtual DbSet<Product> Products { get; set; } = null!;

        public virtual DbSet<Region> Regions { get; set; } = null!;

        public virtual DbSet<Shipper> Shippers { get; set; } = null!;

        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        public virtual DbSet<Territory> Territories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseLazyLoadingProxies();

            if (!dbContextOptionsBuilder.IsConfigured)
            {
                string secretsJSONFileFullPath = DATABASE_CONNECTION_STRING;

                _configurationBuilder = new ConfigurationBuilder()
                   .SetBasePath(Path.Join(secretsJSONFileFullPath))
                   .AddJsonFile(SECRETS_JSON_FILE_NAME);

                _configurationRoot = _configurationBuilder.Build();

                IConfigurationSection? secretsJSONConnectionStringSection = _configurationRoot.GetSection(SECRETS_JSON_CONNECTION_STRING_SECTION);
                string secretsJSONConnectionStringSectionValue = secretsJSONConnectionStringSection.Value;

                dbContextOptionsBuilder.UseSqlServer(secretsJSONConnectionStringSectionValue);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEntityRelations(modelBuilder);
            EntityIndexesConfigurator.Configure(modelBuilder);

            List<IMutableEntityType> mutableEntityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            var deletableMutableEntityTypes = mutableEntityTypes.Where(met => met.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(met.ClrType));

            foreach (var deletableMutableEntityType in deletableMutableEntityTypes)
            {
                var setIsDeletedQueryFilterGenericMethod = SetIsDeletedQueryFilterMethodInfo.MakeGenericMethod(deletableMutableEntityType.ClrType);
                setIsDeletedQueryFilterGenericMethod.Invoke(null, new object[] { modelBuilder });
            }

            var foreignKeysForCascadeDeleteBehavior = deletableMutableEntityTypes
                .SelectMany(e => e.GetForeignKeys().Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKeyForCascadeDeleteBehavior in foreignKeysForCascadeDeleteBehavior)
            {
                foreignKeyForCascadeDeleteBehavior.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges() => SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyEntityChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyEntityChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyEntityChanges()
        {
            List<EntityEntry<BaseDeletableEntity>> changeTrackerEntityEntries = ChangeTracker.Entries<BaseDeletableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            SequentialGuidValueGenerator sequentialGuidValueGenerator = new();

            if (changeTrackerEntityEntries.Any())
            {
                foreach (var changeTrackerEntityEntry in changeTrackerEntityEntries)
                {
                    switch (changeTrackerEntityEntry.State)
                    {
                        case EntityState.Added:
                            if (string.IsNullOrWhiteSpace(changeTrackerEntityEntry.Entity.Id))
                            {
                                changeTrackerEntityEntry.Entity.Id = sequentialGuidValueGenerator.Next(changeTrackerEntityEntry).ToString()[..8];
                            }

                            if (changeTrackerEntityEntry.Entity.CreatedAt == default)
                            {
                                changeTrackerEntityEntry.Entity.CreatedAt = DateTime.UtcNow;
                            }
                            break;
                        case EntityState.Modified:
                            changeTrackerEntityEntry.Entity.ModifiedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }

        private static void SetIsDeletedQueryFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, IDeletableEntity
            => modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);

        private void ConfigureEntityRelations(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}