using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Settings;
using nexus_bilding_api.infrastructure.shared.Email;

namespace nexus_bilding_api.infrastructure.shared;

public static class ServiceExtensionIOC
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}