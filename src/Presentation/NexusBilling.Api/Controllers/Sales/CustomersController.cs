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

namespace NexusBilling.Api.Controllers.Sales;

public record UpsertCustomerRequest(
    string? No,
    string Name,
    string Address,
    string City,
    string Contact,
    string PhoneNo,
    string Email,
    decimal CreditLimit,
    string VatRegistrationNo,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string CurrencyCode,
    string CustomerPostingGroup,
    string CountryRegionCode
);

[Authorize]
[ApiController]
[Route("api/v1/sales/customers")]
public sealed class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? search,
        [FromQuery] bool? blocked,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetCustomersQuery(tenantId, search, blocked, page, pageSize),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(new
        {
            items = result.Items,
            pagination = new
            {
                page,
                pageSize,
                totalItems = result.TotalItems,
                totalPages = result.TotalPages
            }
        }));
    }

    [HttpGet("{no}")]
    public async Task<IActionResult> GetByNo(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var customer = await mediator.Send(new GetCustomerByNoQuery(tenantId, no), cancellationToken);

        if (customer is null)
            return NotFound(ApiResponse<object?>.NotFound($"El cliente {no} no fue encontrado."));

        return Ok(ApiResponse<object>.Ok(customer));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertCustomerRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new UpsertCustomerCommand(tenantId, null, req.No, req.Name, req.Address, req.City,
            req.Contact, req.PhoneNo, req.Email, req.CreditLimit, req.VatRegistrationNo,
            req.PaymentTermsCode, req.PaymentMethodCode, req.SalespersonCode,
            req.CurrencyCode, req.CustomerPostingGroup, req.CountryRegionCode);

        var result = await mediator.Send(cmd, cancellationToken);
        return result.Created
            ? CreatedAtAction(nameof(GetByNo), new { no = result.No },
                ApiResponse<object>.Ok(new { no = result.No }))
            : Conflict(ApiResponse<object?>.Fail("CONFLICT", $"El cliente {req.No} ya existe."));
    }

    [HttpPut("{no}")]
    public async Task<IActionResult> Update(string no, [FromBody] UpsertCustomerRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new UpsertCustomerCommand(tenantId, no, req.No, req.Name, req.Address, req.City,
            req.Contact, req.PhoneNo, req.Email, req.CreditLimit, req.VatRegistrationNo,
            req.PaymentTermsCode, req.PaymentMethodCode, req.SalespersonCode,
            req.CurrencyCode, req.CustomerPostingGroup, req.CountryRegionCode);

        var result = await mediator.Send(cmd, cancellationToken);
        return result.Created
            ? NotFound(ApiResponse<object?>.NotFound($"El cliente {no} no fue encontrado."))
            : Ok(ApiResponse<object>.Ok(new { no = result.No }));
    }

    [HttpPatch("{no}/block")]
    public async Task<IActionResult> Block(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var found = await mediator.Send(new SetCustomerBlockedCommand(tenantId, no, true), cancellationToken);
        return found
            ? Ok(ApiResponse<object>.Ok(new { no, blocked = true }))
            : NotFound(ApiResponse<object?>.NotFound($"El cliente {no} no fue encontrado."));
    }

    [HttpPatch("{no}/unblock")]
    public async Task<IActionResult> Unblock(string no, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var found = await mediator.Send(new SetCustomerBlockedCommand(tenantId, no, false), cancellationToken);
        return found
            ? Ok(ApiResponse<object>.Ok(new { no, blocked = false }))
            : NotFound(ApiResponse<object?>.NotFound($"El cliente {no} no fue encontrado."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
