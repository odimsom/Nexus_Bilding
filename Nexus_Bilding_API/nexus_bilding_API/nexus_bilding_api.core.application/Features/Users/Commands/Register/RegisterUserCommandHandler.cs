using nexus_bilding_api.core.application.DTOs.Account;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Features.Users.Commands.Register;

public interface IRegisterUserCommandHandler
{
    Task<Result<string>> Handle(RegisterRequest request, string origin);
}

public class RegisterUserCommandHandler : IRegisterUserCommandHandler
{
    private readonly IAccountService _accountService;

    public RegisterUserCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<string>> Handle(RegisterRequest request, string origin)
    {
        return await _accountService.RegisterAsync(request, origin);
    }
}
