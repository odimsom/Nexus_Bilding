using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoiceByNo;
using NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoices;

namespace NexusBilling.Api.Controllers.Sales;

[Authorize]
[ApiController]
[Route("api/v1/sales/invoices")]
public sealed class SalesInvoicesController(IMediator mediator) : ControllerBase
{
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

        var result = await mediator.Send(new GetSalesInvoicesQuery(tenantId, search, page, pageSize), cancellationToken);

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

        var invoice = await mediator.Send(new GetSalesInvoiceByNoQuery(tenantId, no), cancellationToken);
        return invoice is not null
            ? Ok(ApiResponse<SalesInvoiceDetailDto>.Ok(invoice))
            : NotFound(ApiResponse<object?>.NotFound($"La factura {no} no fue encontrada."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
