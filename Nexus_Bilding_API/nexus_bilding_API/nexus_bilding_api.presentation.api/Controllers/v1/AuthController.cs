using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Account;
using nexus_bilding_api.core.application.Features.Users.Commands.Authenticate;
using nexus_bilding_api.core.application.Features.Users.Commands.Register;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class AuthController : BaseApiController
{
    private readonly IAuthenticateUserCommandHandler _authenticateHandler;
    private readonly IRegisterUserCommandHandler _registerHandler;
    private readonly nexus_bilding_api.core.application.Features.Users.Commands.ConfirmEmail.IConfirmEmailCommandHandler _confirmEmailHandler;

    public AuthController(IAuthenticateUserCommandHandler authenticateHandler, IRegisterUserCommandHandler registerHandler, nexus_bilding_api.core.application.Features.Users.Commands.ConfirmEmail.IConfirmEmailCommandHandler confirmEmailHandler)
    {
        _authenticateHandler = authenticateHandler;
        _registerHandler = registerHandler;
        _confirmEmailHandler = confirmEmailHandler;
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return HandleResult(await _confirmEmailHandler.Handle(userId, code));
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
    {
        return HandleResult(await _authenticateHandler.Handle(request, GenerateIPAddress()));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        return HandleResult(await _registerHandler.Handle(request, Request.Headers["origin"]!));
    }

    private string GenerateIPAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"]!;
        else
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "127.0.0.1";
    }
}
