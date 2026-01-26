using nexus_bilding_api.core.application.DTOs.Email;

using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Interfaces;

public interface IEmailService
{
    Task<Result<Unit>> SendAsync(EmailRequestDTO request);
}