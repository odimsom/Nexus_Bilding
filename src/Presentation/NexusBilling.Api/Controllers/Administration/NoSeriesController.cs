using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Administration.Commands;
using NexusBilling.Core.Application.Administration.Queries;

namespace NexusBilling.Api.Controllers.Administration;

public record UpsertNoSeriesRequest(string Code, string Description, bool DefaultNos, bool ManualNos);
public record UpsertNoSeriesLineRequest(string SeriesCode, string StartingNo, string EndingNo, int IncrementByNo);

[Authorize]
[ApiController]
[Route("api/v1/administration/no-series")]
public sealed class NoSeriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetNoSeriesQuery(tenantId), cancellationToken);
        return Ok(ApiResponse<object>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertNoSeriesRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(
            new UpsertNoSeriesCommand(tenantId, req.Code, req.Description, req.DefaultNos, req.ManualNos),
            cancellationToken);

        return ok ? Ok(ApiResponse<object>.Ok(new { code = req.Code }))
                  : BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", "No se pudo guardar la serie."));
    }

    [HttpPost("lines")]
    public async Task<IActionResult> UpsertLine([FromBody] UpsertNoSeriesLineRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(
            new UpsertNoSeriesLineCommand(tenantId, req.SeriesCode, req.StartingNo, req.EndingNo, req.IncrementByNo),
            cancellationToken);

        return ok ? Ok(ApiResponse<object>.Ok(new { seriesCode = req.SeriesCode }))
                  : BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", "No se pudo guardar la línea."));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(new DeleteNoSeriesCommand(tenantId, code), cancellationToken);
        return ok ? Ok(ApiResponse<object>.Ok(new { code }))
                  : NotFound(ApiResponse<object?>.NotFound($"La serie {code} no fue encontrada."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
