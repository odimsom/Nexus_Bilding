using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Finance.Features.GLEntries.Queries.GetGLEntries;

namespace NexusBilling.Api.Controllers.Finance;

[Authorize]
[ApiController]
[Route("api/v1/finance/gl-entries")]
public sealed class GLEntriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? glAccountNo,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetGLEntriesQuery(tenantId, glAccountNo, page, pageSize),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            entries = result.Entries,
            pagination = new
            {
                page,
                pageSize,
                totalItems = result.TotalItems,
                totalPages = result.TotalPages
            }
        }));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
