using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Administration.Commands;
using NexusBilling.Core.Application.Administration.Queries;

namespace NexusBilling.Api.Controllers.Administration;

public record ServiceOrderLineRequest(
    string No,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    short LineType = 1);

public record CreateServiceOrderRequest(
    short DocumentType,
    string CustomerNo,
    string CustomerName,
    string Description,
    DateTime OrderDate,
    DateTime? StartingDate,
    DateTime? FinishingDate,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string CurrencyCode,
    string ContractNo,
    string? SeriesCode,
    string? ManualNo,
    IReadOnlyList<ServiceOrderLineRequest> Lines);

[Authorize]
[ApiController]
[Route("api/v1/service/orders")]
public sealed class ServiceOrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? documentType,
        [FromQuery] string? status,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetServiceOrdersQuery(tenantId, documentType, status, search, page, pageSize),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            items = result.Items,
            pagination = new { page, pageSize, totalItems = result.TotalCount, totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize) }
        }));
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var order = await mediator.Send(new GetServiceOrderByNoQuery(tenantId, no), cancellationToken);
        return order is not null
            ? Ok(ApiResponse<ServiceOrderDetailDto>.Ok(order))
            : NotFound(ApiResponse<object?>.NotFound($"La orden de servicio {no} no fue encontrada."));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceOrderRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var lines = req.Lines.Select(l => new ServiceOrderLineInput(
            l.No, l.Description, l.Quantity, l.UnitPrice, l.LineDiscountPct, l.UnitOfMeasure, l.LineType))
            .ToList();

        var cmd = new CreateServiceOrderCommand(
            tenantId, req.DocumentType, req.CustomerNo, req.CustomerName,
            req.Description ?? string.Empty, req.OrderDate, req.StartingDate, req.FinishingDate,
            req.PaymentTermsCode ?? string.Empty, req.PaymentMethodCode ?? string.Empty,
            req.SalespersonCode ?? string.Empty, req.CurrencyCode ?? string.Empty,
            req.ContractNo ?? string.Empty, req.SeriesCode, req.ManualNo, lines);

        try
        {
            var result = await mediator.Send(cmd, cancellationToken);
            return CreatedAtAction(nameof(GetByNo), new { no = result.No },
                ApiResponse<object>.Ok(new { no = result.No }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
        catch (DbUpdateException dbEx)
        {
            var inner = dbEx.InnerException?.Message ?? dbEx.Message;
            if (inner.Contains("duplicate") || inner.Contains("unique") || inner.Contains("23505"))
                return Conflict(ApiResponse<object?>.Fail("CONFLICT", "El número de documento ya existe."));
            return BadRequest(ApiResponse<object?>.Fail("DB_ERROR", "Error al guardar el documento. Verifica los datos e intenta de nuevo."));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", "Error interno del servidor. Intenta de nuevo."));
        }
    }

    [HttpPatch("{no}/status")]
    public async Task<IActionResult> UpdateStatus(string no, [FromBody] UpdateServiceStatusRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        // For now, return not implemented — future: update status
        return Ok(ApiResponse<object>.Ok(new { no, status = req.Status }));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}

public record UpdateServiceStatusRequest(short Status);
