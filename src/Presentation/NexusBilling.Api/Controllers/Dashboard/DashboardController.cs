using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Dashboard.Queries;

namespace NexusBilling.Api.Controllers.Dashboard;

[Authorize]
[ApiController]
[Route("api/v1/dashboard")]
public sealed class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(CancellationToken ct)
    {
        var tenantId = GetTenantId();
        var result = await mediator.Send(new GetDashboardStatsQuery(tenantId), ct);
        return Ok(ApiResponse<DashboardStatsDto>.Ok(result));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
