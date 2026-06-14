using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Administration.Features.ServiceOrders.Commands.CreateServiceOrder;
using NexusBilling.Core.Application.Administration.Features.NoSeries.Commands.UpsertNoSeries;
using NexusBilling.Core.Application.Administration.Features.NoSeries.Queries.GetNoSeries;
using NexusBilling.Core.Application.Administration.Features.ServiceOrders.Queries.GetServiceOrderByNo;
using NexusBilling.Core.Application.Administration.Features.ServiceOrders.Queries.GetServiceOrders;
using NexusBilling.Core.Application.Dashboard.Features.DashboardStats.Queries.GetDashboardStats;
using NexusBilling.Core.Application.Inventory.Features.Items.Commands.AdjustInventory;
using NexusBilling.Core.Application.Inventory.Features.Items.Commands.SetItemBlocked;
using NexusBilling.Core.Application.Inventory.Features.Items.Commands.UpsertItem;
using NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItemByNo;
using NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItemLedgerEntries;
using NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItems;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.CreateVendor;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrders;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendors;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.CreateSalesOrder;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.PostSalesOrder;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.ReleaseSalesOrder;
using NexusBilling.Core.Application.Sales.Features.Customers.Commands.SetCustomerBlocked;
using NexusBilling.Core.Application.Sales.Features.Customers.Commands.UpsertCustomer;
using NexusBilling.Core.Application.Sales.Features.Customers.Queries.GetCustomerByNo;
using NexusBilling.Core.Application.Sales.Features.Customers.Queries.GetCustomers;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Queries.GetSalesOrderByNo;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Queries.GetSalesOrders;
using NexusBilling.Core.Application.Security.Features.Users.Commands.CreateUser;
using NexusBilling.Core.Application.Security.Features.Users.Commands.Login;
using NexusBilling.Core.Application.Security.Features.Users.Commands.Logout;
using NexusBilling.Core.Application.Security.Features.Users.Commands.RefreshToken;
using NexusBilling.Core.Application.Security.Features.Users.Commands.UpdateUser;
using NexusBilling.Core.Application.Security.Features.Users.Queries.GetActiveSessions;
using NexusBilling.Core.Application.Security.Features.Users.Queries.GetCurrentUser;
using NexusBilling.Core.Application.Security.Features.Users.Queries.GetUserGroups;
using NexusBilling.Core.Application.Security.Features.Users.Queries.GetUserSetup;
using NexusBilling.Core.Application.Security.Features.Users.Queries.GetUsers;

using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrderByNo;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.CreatePurchaseOrder;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderLines;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.PostPurchaseOrder;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderHeader;

namespace NexusBilling.Api.Controllers.Purchasing;

public record PurchaseOrderLineRequest(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    string LineType = "Item");

public record CreatePurchaseOrderRequest(
    string BuyFromVendorNo,
    string? PayToName,
    DateTime? PostingDate,
    DateTime? DueDate,
    string? CurrencyCode,
    string? PaymentTermsCode,
    string? ExternalDocumentNo,
    List<PurchaseOrderLineDto>? Lines);

[Authorize]
[ApiController]
[Route("api/v1/purchasing/orders")]
public class PurchaseOrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetPurchaseOrdersQuery(tenantId, page, pageSize), cancellationToken);

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

        var result = await mediator.Send(new GetPurchaseOrderByNoQuery(tenantId, no), cancellationToken);

        if (result == null)
            return NotFound(ApiResponse<object?>.NotFound($"El pedido de compra {no} no fue encontrado."));

        return Ok(ApiResponse<PurchaseOrderDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new CreatePurchaseOrderCommand(
            tenantId,
            null,
            "PC",
            req.BuyFromVendorNo,
            req.PayToName ?? "",
            req.PostingDate ?? DateTime.UtcNow,
            req.DueDate,
            req.CurrencyCode,
            req.PaymentTermsCode,
            req.ExternalDocumentNo,
            req.Lines ?? []
        );

        try
        {
            var result = await mediator.Send(cmd, cancellationToken);
            return Ok(ApiResponse<object>.Ok(new { no = result.No }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
    }

    [HttpPatch("{no}")]
    public async Task<IActionResult> UpdateHeader(string no, [FromBody] UpdatePurchaseOrderHeaderRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            await mediator.Send(new UpdatePurchaseOrderHeaderCommand(
                tenantId, no,
                req.DueDate,
                req.CurrencyCode ?? string.Empty,
                req.PaymentTermsCode ?? string.Empty,
                req.ExternalDocumentNo), cancellationToken);
            return Ok(ApiResponse<object?>.Ok(null));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
    }

    [HttpPut("{no}/lines")]
    public async Task<IActionResult> UpdateLines(string no, [FromBody] IReadOnlyList<PurchaseOrderLineRequest> lines, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var cmd = new UpdatePurchaseOrderLinesCommand(
                tenantId, no,
                lines.Select(l => new PurchaseOrderLineData(
                    l.ItemNo, l.Description, l.Quantity, l.UnitPrice, l.LineDiscountPct, l.UnitOfMeasure, l.LineType))
                .ToList());
            var result = await mediator.Send(cmd, cancellationToken);
            return Ok(ApiResponse<object>.Ok(new { amount = result.Amount, amountIncludingVat = result.AmountIncludingVat }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
    }

    [HttpPost("{no}/post")]
    public async Task<IActionResult> Post(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var result = await mediator.Send(new PostPurchaseOrderCommand(tenantId, no), cancellationToken);
            return Ok(ApiResponse<object>.Ok(new { invoiceNo = result.InvoiceNo }));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
        catch (Exception ex)
        {
            var inner = ex.InnerException?.Message ?? ex.Message;
            return StatusCode(500, ApiResponse<object?>.Fail("INTERNAL_ERROR", $"Error al publicar la orden: {inner}"));
        }
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}

public record UpdatePurchaseOrderHeaderRequest(
    DateTime? DueDate,
    string? CurrencyCode,
    string? PaymentTermsCode,
    string? ExternalDocumentNo);
