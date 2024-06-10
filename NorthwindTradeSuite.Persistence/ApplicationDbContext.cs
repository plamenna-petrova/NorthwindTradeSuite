using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using NorthwindTradeSuite.Domain.Entities;
using NorthwindTradeSuite.Domain.Entities.Identity;
using NorthwindTradeSuite.Domain.Interfaces;

namespace NorthwindTradeSuite.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, 
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private IConfigurationBuilder _configurationBuilder;

        private IConfigurationRoot _configurationRoot;

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Shipper> Shippers { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseLazyLoadingProxies();

            if (!dbContextOptionsBuilder.IsConfigured)
            {
                string secretsJSONFileFullPath = string.Empty;

                _configurationBuilder = new ConfigurationBuilder()
                   .SetBasePath(Path.Join(secretsJSONFileFullPath))
                   .AddJsonFile(string.Empty);

                _configurationRoot = _configurationBuilder.Build();

                dbContextOptionsBuilder.UseSqlServer(_configurationRoot.GetSection(string.Empty).Value);
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
            List<EntityEntry<IAuditInfo>> auditableEntityChangeTrackerEntries = ChangeTracker.Entries<IAuditInfo>().ToList();

            foreach (var auditableEntityChangeTrackerEntry in auditableEntityChangeTrackerEntries)
            {
                switch (auditableEntityChangeTrackerEntry.State)
                {
                    case EntityState.Added:
                        auditableEntityChangeTrackerEntry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        auditableEntityChangeTrackerEntry.Entity.ModifiedOn = DateTime.UtcNow;
                        break;
                }
            }
        }

        private void ConfigureEntityRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
