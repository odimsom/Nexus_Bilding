using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NexusBilling.Core.Application.Administration.Services;

namespace NexusBilling.Core.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<NoSeriesService>();
        return services;
    }
}
