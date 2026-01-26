using System.Reflection;
using Microsoft.EntityFrameworkCore;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.Context;

public class NexusBillingContext : DbContext
{
    public NexusBillingContext(DbContextOptions<NexusBillingContext> options): base(options){}

    #region DbSets
    public DbSet<Client> Clients { get; set; }
    public DbSet<FiscalDocument> FiscalDocuments { get; set; }
    public DbSet<FiscalDocumentItem> FiscalDocumentItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    // public DbSet<User> Users { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}