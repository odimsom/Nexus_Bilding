using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Sales.Features.Quotations.Commands.ConvertQuotationToOrder;
using NexusBilling.Core.Application.Sales.Features.Quotations.Commands.CreateQuotation;
using NexusBilling.Core.Application.Sales.Features.Quotations.Commands.DuplicateQuotation;
using NexusBilling.Core.Application.Sales.Features.Quotations.Commands.UpdateQuotationHeader;
using NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotationByNo;
using NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotations;

namespace NexusBilling.Api.Controllers.Sales;

public record QuotationLineRequest(
    string LineType,
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    decimal VatPct = 18m,
    short? ServiceBillingType = null,
    DateTime? ServiceStartDate = null,
    DateTime? ServiceEndDate = null,
    decimal? ServiceHours = null,
    decimal? HourlyRate = null,
    string? ResourceNo = null);

public record CreateQuotationRequest(
    string SellToCustomerNo,
    string SellToCustomerName,
    DateTime PostingDate,
    DateTime? ValidUntilDate,
    string? QuotedBy,
    string? Observations,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string? ExternalDocumentNo,
    string? SeriesCode,
    IReadOnlyList<QuotationLineRequest> Lines);

public record ConvertToOrderRequest(string? SeriesCode);

public record DuplicateQuotationRequest(
    DateTime NewPostingDate,
    DateTime? NewValidUntilDate,
    string? SeriesCode);

[Authorize]
[ApiController]
[Route("api/v1/sales/quotations")]
public sealed class QuotationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? customerNo,
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var result = await mediator.Send(
            new GetQuotationsQuery(tenantId, customerNo, search, status, page, pageSize), ct);

        return Ok(ApiResponse<object>.Ok(new
        {
            items = result.Items,
            pagination = new { page, pageSize, totalItems = result.TotalCount, totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize) }
        }));
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var detail = await mediator.Send(new GetQuotationByNoQuery(tenantId, no), ct);
        return detail is not null
            ? Ok(ApiResponse<QuotationDetail>.Ok(detail))
            : NotFound(ApiResponse<object?>.NotFound($"Cotización {no} no encontrada."));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuotationRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var lines = req.Lines.Select(l => new QuotationLineInput(
            l.LineType, l.ItemNo, l.Description, l.Quantity, l.UnitPrice,
            l.LineDiscountPct, l.UnitOfMeasure, l.VatPct,
            l.ServiceBillingType, l.ServiceStartDate, l.ServiceEndDate,
            l.ServiceHours, l.HourlyRate, l.ResourceNo)).ToList();

        var cmd = new CreateQuotationCommand(
            tenantId, req.SellToCustomerNo, req.SellToCustomerName,
            req.PostingDate, req.ValidUntilDate, req.QuotedBy, req.Observations,
            req.CurrencyCode ?? string.Empty, req.PaymentTermsCode ?? string.Empty,
            req.PaymentMethodCode ?? string.Empty, req.ExternalDocumentNo,
            req.SeriesCode ?? "COT", lines);

        try
        {
            var result = await mediator.Send(cmd, ct);
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
            if (inner.Contains("duplicate") || inner.Contains("23505"))
                return Conflict(ApiResponse<object?>.Fail("CONFLICT", "El número de documento ya existe."));
            return BadRequest(ApiResponse<object?>.Fail("DB_ERROR", "Error al guardar la cotización."));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", "Error interno del servidor."));
        }
    }

    [HttpPost("{no}/convert-to-order")]
    public async Task<IActionResult> ConvertToOrder(string no, [FromBody] ConvertToOrderRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        try
        {
            var result = await mediator.Send(
                new ConvertQuotationToOrderCommand(tenantId, no, req.SeriesCode ?? "ORD"), ct);
            return Ok(ApiResponse<object>.Ok(new { orderNo = result.OrderNo }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", "Error interno del servidor."));
        }
    }

    [HttpPatch("{no}")]
    public async Task<IActionResult> UpdateHeader(string no, [FromBody] UpdateQuotationHeaderRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        try
        {
            await mediator.Send(new UpdateQuotationHeaderCommand(
                tenantId, no,
                req.ValidUntilDate, req.QuotedBy, req.Observations,
                req.CurrencyCode ?? string.Empty,
                req.PaymentTermsCode ?? string.Empty,
                req.PaymentMethodCode ?? string.Empty,
                req.ExternalDocumentNo), ct);
            return Ok(ApiResponse<object?>.Ok(null));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", "Error interno del servidor."));
        }
    }

    [HttpPost("{no}/duplicate")]
    public async Task<IActionResult> Duplicate(string no, [FromBody] DuplicateQuotationRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        try
        {
            var result = await mediator.Send(
                new DuplicateQuotationCommand(tenantId, no, req.NewPostingDate, req.NewValidUntilDate, req.SeriesCode ?? "COT"), ct);
            return Ok(ApiResponse<object>.Ok(new { no = result.No }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", "Error interno del servidor."));
        }
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}

public record UpdateQuotationHeaderRequest(
    DateTime? ValidUntilDate,
    string? QuotedBy,
    string? Observations,
    string? CurrencyCode,
    string? PaymentTermsCode,
    string? PaymentMethodCode,
    string? ExternalDocumentNo);
