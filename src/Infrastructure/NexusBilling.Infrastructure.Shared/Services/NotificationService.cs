using System.Threading.Tasks;
using NexusBilling.Core.Application.Common.Interfaces;

namespace NexusBilling.Infrastructure.Shared.Services;

public class NotificationService : INotificationService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        await Task.CompletedTask;
    }

    public async Task SendSmsAsync(string number, string message)
    {
        await Task.CompletedTask;
    }
}
