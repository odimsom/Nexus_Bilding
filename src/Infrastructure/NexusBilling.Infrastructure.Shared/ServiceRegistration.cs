using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Core.Application.Common.Interfaces;
using NexusBilling.Infrastructure.Shared.Services;

namespace NexusBilling.Infrastructure.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<INotificationService, NotificationService>();
        return services;
    }
}
