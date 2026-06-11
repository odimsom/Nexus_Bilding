using NexusBilling.Infrastructure.Persistence.Context;
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
        
        // Repositorios
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IGLAccountRepository, GLAccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISalesHeaderRepository, SalesHeaderRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IPurchaseHeaderRepository, PurchaseHeaderRepository>();
        
        return services;
    }
}
