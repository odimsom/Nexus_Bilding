using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccounts;
using NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccountByNo;

namespace NexusBilling.Api.Controllers.Finance;

[Authorize]
[ApiController]
[Route("api/v1/finance/gl-accounts")]
public sealed class GLAccountsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? search,
        [FromQuery] bool? blocked,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetGLAccountsQuery(tenantId, search, blocked, page, pageSize),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            accounts = result.Accounts,
            pagination = new
            {
                page,
                pageSize,
                totalItems = result.TotalItems,
                totalPages = result.TotalPages
            }
        }));
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var account = await mediator.Send(new GetGLAccountByNoQuery(tenantId, no), cancellationToken);

        if (account is null)
            return NotFound(ApiResponse<object?>.NotFound($"La cuenta {no} no fue encontrada."));

        return Ok(ApiResponse<object>.Ok(account));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
