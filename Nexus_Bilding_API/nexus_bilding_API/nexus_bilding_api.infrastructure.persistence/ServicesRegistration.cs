using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence;

public static class ServicesRegistration
{
    public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FCastroConnection");
        services.AddDbContext<NexusBillingContext>(
            opt => opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,44)),
                m =>
                {
                    m.MigrationsAssembly(typeof(NexusBillingContext).Assembly.FullName);
                }
            )
        );
        
        services.AddScoped(typeof(IGenericRepository<>),  typeof(GenericRepository<>));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IFiscalDocumentRepository, FiscalDocumentRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}