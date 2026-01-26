using nexus_bilding_api.core.application.DTOs.Account;
using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Interfaces;

public interface IAccountService
{
    Task<Result<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<Result<string>> RegisterAsync(nexus_bilding_api.core.application.DTOs.Account.RegisterRequest request, string origin);
    Task<Result<string>> ConfirmEmailAsync(string userId, string code);
}
