using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.core.application.Features.Users.Commands.ConfirmEmail;

public interface IConfirmEmailCommandHandler
{
    Task<Result<string>> Handle(string userId, string code);
}

public class ConfirmEmailCommandHandler : IConfirmEmailCommandHandler
{
    private readonly IAccountService _accountService;

    public ConfirmEmailCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result<string>> Handle(string userId, string code)
    {
        return await _accountService.ConfirmEmailAsync(userId, code);
    }
}
