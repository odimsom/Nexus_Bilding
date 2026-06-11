using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;
using NexusBilling.Infrastructure.Persistence.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Security.Repositories;

namespace NexusBilling.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Finance
        services.AddScoped<IBusinessChartUserSetupRepository, BusinessChartUserSetupRepository>();
        services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
        services.AddScoped<IGLAccountCategoryRepository, GLAccountCategoryRepository>();
        services.AddScoped<IGLBudgetNameRepository, GLBudgetNameRepository>();
        services.AddScoped<IGLEntryRepository, GLEntryRepository>();
        services.AddScoped<IGLRegisterRepository, GLRegisterRepository>();
        services.AddScoped<IGenBusinessPostingGroupRepository, GenBusinessPostingGroupRepository>();
        services.AddScoped<IGenJournalBatchRepository, GenJournalBatchRepository>();
        services.AddScoped<IGenJournalLineRepository, GenJournalLineRepository>();
        services.AddScoped<IGenJournalTemplateRepository, GenJournalTemplateRepository>();
        services.AddScoped<IGenProductPostingGroupRepository, GenProductPostingGroupRepository>();
        services.AddScoped<IGeneralLedgerSetupRepository, GeneralLedgerSetupRepository>();
        services.AddScoped<IGeneralPostingSetupRepository, GeneralPostingSetupRepository>();
        services.AddScoped<IMyAccountRepository, MyAccountRepository>();
        services.AddScoped<IRoundingMethodRepository, RoundingMethodRepository>();
        services.AddScoped<IStandardGeneralJournalRepository, StandardGeneralJournalRepository>();
        services.AddScoped<IStandardGeneralJournalLineRepository, StandardGeneralJournalLineRepository>();
        services.AddScoped<ITrialBalanceSetupRepository, TrialBalanceSetupRepository>();
        services.AddScoped<IAccountingPeriodRepository, AccountingPeriodRepository>();
        services.AddScoped<IGLAccountRepository, GLAccountRepository>();

        // Sales
        services.AddScoped<ISalesLineRepository, SalesLineRepository>();
        services.AddScoped<ICustLedgerEntryRepository, CustLedgerEntryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISalesHeaderRepository, SalesHeaderRepository>();

        // Purchasing
        services.AddScoped<IPurchaseLineRepository, PurchaseLineRepository>();
        services.AddScoped<IVendorLedgerEntryRepository, VendorLedgerEntryRepository>();
        services.AddScoped<IPurchaseHeaderRepository, PurchaseHeaderRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();

        // Inventory
        services.AddScoped<IItemLedgerEntryRepository, ItemLedgerEntryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemUnitOfMeasureRepository, ItemUnitOfMeasureRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        // Security
        services.AddScoped<IUserRepository, UserRepository>();

        // Administration
        services.AddScoped<ITenantRepository, TenantRepository>();
        
        return services;
    }
}
