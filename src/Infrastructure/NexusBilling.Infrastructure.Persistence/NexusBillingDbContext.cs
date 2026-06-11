using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Inventory.Entities;
using System.Reflection;

namespace NexusBilling.Infrastructure.Persistence;

public class NexusBillingDbContext : DbContext
{
    public NexusBillingDbContext(DbContextOptions<NexusBillingDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("erp");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
