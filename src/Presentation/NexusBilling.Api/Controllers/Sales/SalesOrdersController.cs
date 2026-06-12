using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Sales.Commands;
using NexusBilling.Core.Application.Sales.Queries;

namespace NexusBilling.Api.Controllers.Sales;

public record SalesOrderLineRequest(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure);

public record CreateSalesOrderRequest(
    string DocumentType,
    string SellToCustomerNo,
    string SellToCustomerName,
    string ExternalDocumentNo,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    DateTime PostingDate,
    DateTime? DueDate,
    string? SeriesCode,
    string? ManualNo,
    IReadOnlyList<SalesOrderLineRequest> Lines);

[Authorize]
[ApiController]
[Route("api/v1/sales/orders")]
public sealed class SalesOrdersController(IMediator mediator) : ControllerBase
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
            new GetSalesOrdersQuery(tenantId, documentType, status, search, page, pageSize),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            items = result.Items,
            pagination = new { page, pageSize, totalItems = result.TotalCount, totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize) }
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSalesOrderRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var lines = req.Lines.Select(l => new SalesOrderLineInput(
            l.ItemNo, l.Description, l.Quantity, l.UnitPrice, l.LineDiscountPct, l.UnitOfMeasure))
            .ToList();

        var cmd = new CreateSalesOrderCommand(
            tenantId, req.DocumentType, req.SellToCustomerNo, req.SellToCustomerName,
            req.ExternalDocumentNo ?? string.Empty, req.CurrencyCode ?? string.Empty,
            req.PaymentTermsCode ?? string.Empty, req.PaymentMethodCode ?? string.Empty,
            req.SalespersonCode ?? string.Empty, req.PostingDate, req.DueDate,
            req.SeriesCode, req.ManualNo, lines);

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
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var order = await mediator.Send(new GetSalesOrderByNoQuery(tenantId, no), cancellationToken);
        return order is not null
            ? Ok(ApiResponse<SalesOrderDetailDto>.Ok(order))
            : NotFound(ApiResponse<object?>.NotFound($"La orden {no} no fue encontrada."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
