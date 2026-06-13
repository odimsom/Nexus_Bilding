using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Commands.CreatePurchaseInvoice;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoiceByNo;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoices;
using CreateInvoiceLineDto = NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Commands.CreatePurchaseInvoice.PurchaseInvoiceLineDto;

namespace NexusBilling.Api.Controllers.Purchasing;

public record CreatePurchaseInvoiceRequest(
    string BuyFromVendorNo,
    string? PayToName,
    DateTime? PostingDate,
    string? ExternalDocumentNo,
    string? PaymentTermsCode,
    string? CurrencyCode,
    List<CreatePurchaseInvoiceLineRequest>? Lines);

public record CreatePurchaseInvoiceLineRequest(
    string Description,
    decimal Quantity,
    decimal UnitCost,
    string? UnitOfMeasureCode,
    decimal VatPct);

[Authorize]
[ApiController]
[Route("api/v1/purchasing/invoices")]
public sealed class PurchaseInvoicesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseInvoiceRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var cmd = new CreatePurchaseInvoiceCommand(
                tenantId,
                req.BuyFromVendorNo,
                req.PayToName ?? string.Empty,
                req.PostingDate ?? DateTime.UtcNow,
                req.ExternalDocumentNo,
                req.PaymentTermsCode,
                req.CurrencyCode,
                (req.Lines ?? []).Select(l => new CreateInvoiceLineDto(
                    l.Description,
                    l.Quantity,
                    l.UnitCost,
                    l.UnitOfMeasureCode ?? string.Empty,
                    l.VatPct)).ToList());

            var result = await mediator.Send(cmd, cancellationToken);
            return Ok(ApiResponse<object>.Ok(new { No = result.InvoiceNo }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION_ERROR", ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetPurchaseInvoicesQuery(tenantId, search, page, pageSize), cancellationToken);

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

        var invoice = await mediator.Send(new GetPurchaseInvoiceByNoQuery(tenantId, no), cancellationToken);
        return invoice is not null
            ? Ok(ApiResponse<PurchaseInvoiceDetailDto>.Ok(invoice))
            : NotFound(ApiResponse<object?>.NotFound($"La factura de compra {no} no fue encontrada."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
