using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Finance.DTOs;
using NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.CreateGenJournalLine;
using NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.DeleteGenJournalLine;
using NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.PostGenJournalLine;
using NexusBilling.Core.Application.Finance.Features.GenJournalLines.Queries.GetGenJournalLines;

namespace NexusBilling.Api.Controllers.Finance;

[Authorize]
[ApiController]
[Route("api/v1/finance/journal")]
public sealed class GenJournalLinesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string templateName = "GENERAL",
        [FromQuery] string batchName = "DEFAULT",
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetGenJournalLinesQuery(tenantId, templateName, batchName),
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<GenJournalLineDto>>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateGenJournalLineCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            command with { TenantId = tenantId },
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail(result.GetError()!.Code, result.GetError().Message));

        return Ok(ApiResponse<Guid>.Ok(result.GetValue()));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new DeleteGenJournalLineCommand(id), cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail(result.GetError()!.Code, result.GetError().Message));

        return Ok(ApiResponse<bool>.Ok(true));
    }

    [HttpPost("post-batch")]
    public async Task<IActionResult> PostBatch(
        [FromQuery] string templateName = "GENERAL",
        [FromQuery] string batchName = "DEFAULT",
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new PostGenJournalLineCommand(tenantId, templateName, batchName),
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail(result.GetError()!.Code, result.GetError().Message));

        return Ok(ApiResponse<object>.Ok(new { postedEntries = result.GetValue() }));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
