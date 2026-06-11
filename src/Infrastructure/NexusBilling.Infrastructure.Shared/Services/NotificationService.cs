using System.Threading.Tasks;
using NexusBilling.Core.Application.Common.Interfaces;

namespace NexusBilling.Infrastructure.Shared.Services;

public class NotificationService : INotificationService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Implementación de email (SMTP, SendGrid, etc.)
        await Task.CompletedTask;
    }

    public async Task SendSmsAsync(string number, string message)
    {
        // Implementación de SMS (Twilio, etc.)
        await Task.CompletedTask;
    }
}
