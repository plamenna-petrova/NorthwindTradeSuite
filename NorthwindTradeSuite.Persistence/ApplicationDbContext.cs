﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using NorthwindTradeSuite.Domain.Abstraction;
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
            List<EntityEntry<BaseEntity<string>>> changeTrackerEntityEntriesWithStringId = ChangeTracker.Entries<BaseEntity<string>>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            List<EntityEntry<BaseEntity<Guid>>> changeTrackerEntityEntriesWithGuidId = ChangeTracker.Entries<BaseEntity<Guid>>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            SequentialGuidValueGenerator sequentialGuidValueGenerator = new();

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

            foreach (var changeTrackerEntityEntryWithGuidId in changeTrackerEntityEntriesWithGuidId)
            {
                switch (changeTrackerEntityEntryWithGuidId.State)
                {
                    case EntityState.Added:
                        if (changeTrackerEntityEntryWithGuidId.Entity.Id == default)
                        {
                            changeTrackerEntityEntryWithGuidId.Entity.Id = sequentialGuidValueGenerator.Next(changeTrackerEntityEntryWithGuidId);
                        }

                        changeTrackerEntityEntryWithGuidId.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        changeTrackerEntityEntryWithGuidId.Entity.ModifiedAt = DateTime.UtcNow;
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
