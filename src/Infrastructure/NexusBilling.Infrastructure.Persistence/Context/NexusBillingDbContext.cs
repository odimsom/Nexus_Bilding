using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Purchasing.Entities;
using System.Reflection;

namespace NexusBilling.Infrastructure.Persistence.Context;

public class NexusBillingDbContext : DbContext
{
    public NexusBillingDbContext(DbContextOptions<NexusBillingDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    
    // Inventory
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemUnitOfMeasure> ItemUnitOfMeasures => Set<ItemUnitOfMeasure>();
    public DbSet<Location> Locations => Set<Location>();
    
    // Finance
    public DbSet<GLAccount> GLAccounts => Set<GLAccount>();
    
    // Sales
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<SalesHeader> SalesHeaders => Set<SalesHeader>();
    
    // Purchasing
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<PurchaseHeader> PurchaseHeaders => Set<PurchaseHeader>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
