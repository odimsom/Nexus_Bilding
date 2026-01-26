using nexus_bilding_api.core.application.DTOs.Account;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Features.Users.Commands.Authenticate;

public interface IAuthenticateUserCommandHandler
{
    Task<Result<AuthenticationResponse>> Handle(AuthenticationRequest request, string ipAddress);
}

public class AuthenticateUserCommandHandler : IAuthenticateUserCommandHandler
{
    private readonly IAccountService _accountService;

    public AuthenticateUserCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<AuthenticationResponse>> Handle(AuthenticationRequest request, string ipAddress)
    {
        return await _accountService.AuthenticateAsync(request, ipAddress);
    }
}
