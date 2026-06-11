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

    // Finance
    public DbSet<BusinessChartUserSetup> BusinessChartUserSetups => Set<BusinessChartUserSetup>();
    public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
    public DbSet<GLAccountCategory> GLAccountCategories => Set<GLAccountCategory>();
    public DbSet<GLBudgetName> GLBudgetNames => Set<GLBudgetName>();
    public DbSet<GLEntry> GLEntries => Set<GLEntry>();
    public DbSet<GLRegister> GLRegisters => Set<GLRegister>();
    public DbSet<GenBusinessPostingGroup> GenBusinessPostingGroups => Set<GenBusinessPostingGroup>();
    public DbSet<GenJournalBatch> GenJournalBatchs => Set<GenJournalBatch>();
    public DbSet<GenJournalLine> GenJournalLines => Set<GenJournalLine>();
    public DbSet<GenJournalTemplate> GenJournalTemplates => Set<GenJournalTemplate>();
    public DbSet<GenProductPostingGroup> GenProductPostingGroups => Set<GenProductPostingGroup>();
    public DbSet<GeneralLedgerSetup> GeneralLedgerSetups => Set<GeneralLedgerSetup>();
    public DbSet<GeneralPostingSetup> GeneralPostingSetups => Set<GeneralPostingSetup>();
    public DbSet<MyAccount> MyAccounts => Set<MyAccount>();
    public DbSet<RoundingMethod> RoundingMethods => Set<RoundingMethod>();
    public DbSet<StandardGeneralJournal> StandardGeneralJournals => Set<StandardGeneralJournal>();
    public DbSet<StandardGeneralJournalLine> StandardGeneralJournalLines => Set<StandardGeneralJournalLine>();
    public DbSet<TrialBalanceSetup> TrialBalanceSetups => Set<TrialBalanceSetup>();
    public DbSet<AccountingPeriod> AccountingPeriods => Set<AccountingPeriod>();
    public DbSet<GLAccount> GLAccounts => Set<GLAccount>();

    // Sales
    public DbSet<SalesLine> SalesLines => Set<SalesLine>();
    public DbSet<CustLedgerEntry> CustLedgerEntries => Set<CustLedgerEntry>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<SalesHeader> SalesHeaders => Set<SalesHeader>();

    // Purchasing
    public DbSet<PurchaseLine> PurchaseLines => Set<PurchaseLine>();
    public DbSet<VendorLedgerEntry> VendorLedgerEntries => Set<VendorLedgerEntry>();
    public DbSet<PurchaseHeader> PurchaseHeaders => Set<PurchaseHeader>();
    public DbSet<Vendor> Vendors => Set<Vendor>();

    // Inventory
    public DbSet<ItemLedgerEntry> ItemLedgerEntries => Set<ItemLedgerEntry>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemUnitOfMeasure> ItemUnitOfMeasures => Set<ItemUnitOfMeasure>();
    public DbSet<Location> Locations => Set<Location>();

    // Security
    public DbSet<User> Users => Set<User>();

    // Administration
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
