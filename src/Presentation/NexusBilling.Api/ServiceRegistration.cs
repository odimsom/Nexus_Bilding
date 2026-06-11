using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Infrastructure.Persistence.Context;

namespace NexusBilling.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NexusBillingDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddOpenApi();
        
        // Aquí irán políticas de CORS, Auth, etc.
        
        return services;
    }
}
