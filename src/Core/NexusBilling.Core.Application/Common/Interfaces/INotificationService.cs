using System.Threading.Tasks;

namespace NexusBilling.Core.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendSmsAsync(string number, string message);
}
