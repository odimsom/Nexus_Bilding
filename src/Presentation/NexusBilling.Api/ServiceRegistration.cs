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
        services.AddCors(options =>
        {
            options.AddPolicy("NexusBillingCors", policy =>
            {
                var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                    ?? ["http://localhost:4200"];

                policy
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddDbContext<NexusBillingDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddApplicationLayer();
        services.AddPersistenceInfrastructure();
        services.AddSharedInfrastructure();
        services.AddIdentityInfrastructure(configuration);

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
