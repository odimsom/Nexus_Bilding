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
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendorByNo;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.UpsertVendor;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.SetVendorBlocked;
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

namespace NexusBilling.Api.Controllers.Purchasing;

public record CreateVendorRequest(
    string Name,
    string Address,
    string City,
    string Contact);

[Authorize]
[ApiController]
[Route("api/v1/purchasing/vendors")]
public class VendorsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVendors(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetVendorsQuery(tenantId, page, pageSize), cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            items = result.Items,
            pagination = new { page, pageSize, totalItems = result.TotalCount, totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize) }
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVendorRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new CreateVendorCommand(tenantId, req.Name, req.Address ?? "", req.City ?? "", req.Contact ?? "");
        var no = await mediator.Send(cmd, cancellationToken);

        return Ok(ApiResponse<object>.Ok(new { No = no }));
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetVendorByNoQuery(tenantId, no), cancellationToken);

        if (result == null)
            return NotFound(ApiResponse<object?>.NotFound($"El proveedor {no} no fue encontrado."));

        return Ok(ApiResponse<VendorDto>.Ok(result));
    }

    [HttpPut("{no}")]
    public async Task<IActionResult> Update(string no, [FromBody] CreateVendorRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new UpsertVendorCommand(tenantId, no, no, req.Name, req.Address ?? "", req.City ?? "", req.Contact ?? "");
        await mediator.Send(cmd, cancellationToken);

        return Ok(ApiResponse<object>.Ok(new { No = no }));
    }

    [HttpPatch("{no}/block")]
    public async Task<IActionResult> Block(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new SetVendorBlockedCommand(tenantId, no, true), cancellationToken);
        if (!result)
            return NotFound(ApiResponse<object?>.NotFound($"El proveedor {no} no fue encontrado."));

        return Ok(ApiResponse<object>.Ok(new { success = true }));
    }

    [HttpPatch("{no}/unblock")]
    public async Task<IActionResult> Unblock(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new SetVendorBlockedCommand(tenantId, no, false), cancellationToken);
        if (!result)
            return NotFound(ApiResponse<object?>.NotFound($"El proveedor {no} no fue encontrado."));

        return Ok(ApiResponse<object>.Ok(new { success = true }));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
