using Microsoft.Extensions.DependencyInjection;

namespace NexusBilling.Infrastructure.Identity;

public static class ServiceRegistration
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
