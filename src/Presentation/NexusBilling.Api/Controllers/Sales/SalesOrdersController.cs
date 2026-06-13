using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.UpdateSalesOrderLines;
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

namespace NexusBilling.Api.Controllers.Sales;

public record SalesOrderLineRequest(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    string LineType = "Item");

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
            l.ItemNo, l.Description, l.Quantity, l.UnitPrice, l.LineDiscountPct, l.UnitOfMeasure, l.LineType))
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

    [HttpPatch("{no}/release")]
    public async Task<IActionResult> Release(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var found = await mediator.Send(new ReleaseSalesOrderCommand(tenantId, no), cancellationToken);
        return found
            ? Ok(ApiResponse<object>.Ok(new { no, status = "Released" }))
            : NotFound(ApiResponse<object?>.NotFound($"La orden {no} no fue encontrada."));
    }

    [HttpPut("{no}/lines")]
    public async Task<IActionResult> UpdateLines(string no, [FromBody] IReadOnlyList<SalesOrderLineRequest> lines, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var cmd = new UpdateSalesOrderLinesCommand(
                tenantId, no,
                lines.Select(l => new SalesOrderLineData(
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
            var result = await mediator.Send(new PostSalesOrderCommand(tenantId, no), cancellationToken);
            return Ok(ApiResponse<object>.Ok(result));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", ex.Message));
        }
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
