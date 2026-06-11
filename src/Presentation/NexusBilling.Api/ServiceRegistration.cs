using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Core.Application;
using NexusBilling.Infrastructure.Persistence;
using NexusBilling.Infrastructure.Shared;
using NexusBilling.Infrastructure.Identity;

namespace NexusBilling.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NexusBillingDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Registro de Capas
        services.AddApplicationLayer();
        services.AddPersistenceInfrastructure();
        services.AddSharedInfrastructure();
        services.AddIdentityInfrastructure();

        services.AddOpenApi();
        
        return services;
    }
}
