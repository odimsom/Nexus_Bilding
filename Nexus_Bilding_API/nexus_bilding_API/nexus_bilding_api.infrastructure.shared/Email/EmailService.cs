using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using nexus_bilding_api.core.application.DTOs.Email;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Settings;

namespace nexus_bilding_api.infrastructure.shared.Email;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task<Result<Unit>> SendAsync(EmailRequestDTO emailRequest)
    {
        try
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
            email.From.Add(MailboxAddress.Parse(_mailSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = emailRequest.Body
            };
            email.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);

            return Result<Unit>.Ok(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while sending email to {To}", emailRequest.To);
            return Result<Unit>.Fail($"Error sending email: {ex.Message}");
        }
    }
}