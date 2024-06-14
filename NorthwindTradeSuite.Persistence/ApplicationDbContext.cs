using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using NorthwindTradeSuite.Domain.Abstraction;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.Identity;
using static NorthwindTradeSuite.Common.GlobalConstants.ConnectionConstants;

namespace NorthwindTradeSuite.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, 
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private IConfigurationBuilder _configurationBuilder = null!;

        private IConfigurationRoot _configurationRoot = null!;

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

                var section = _configurationRoot.GetSection(SECRETS_JSON_CONNECTION_STRING_SECTION);

                string secretsJSONConnectionStringSectionValue = _configurationRoot.GetSection(SECRETS_JSON_CONNECTION_STRING_SECTION).Value;

                dbContextOptionsBuilder.UseSqlServer(secretsJSONConnectionStringSectionValue);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureEntityRelations(modelBuilder);
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyEntityChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(true, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyEntityChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyEntityChanges()
        {
            List<EntityEntry<BaseDeletableEntity<string>>> changeTrackerEntityEntriesWithStringId = ChangeTracker.Entries<BaseDeletableEntity<string>>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            SequentialGuidValueGenerator sequentialGuidValueGenerator = new();

            if (changeTrackerEntityEntriesWithStringId.Any())
            {
                foreach (var changeTrackerEntityEntryWithStringId in changeTrackerEntityEntriesWithStringId)
                {
                    switch (changeTrackerEntityEntryWithStringId.State)
                    {
                        case EntityState.Added:
                            if (string.IsNullOrWhiteSpace(changeTrackerEntityEntryWithStringId.Entity.Id))
                            {
                                changeTrackerEntityEntryWithStringId.Entity.Id = sequentialGuidValueGenerator
                                    .Next(changeTrackerEntityEntryWithStringId)
                                    .ToString()[..7];
                            }

                            changeTrackerEntityEntryWithStringId.Entity.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            changeTrackerEntityEntryWithStringId.Entity.ModifiedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }

        private void ConfigureEntityRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
