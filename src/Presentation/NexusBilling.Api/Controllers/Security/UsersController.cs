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

namespace NexusBilling.Api.Controllers.Security;

public record CreateUserRequest(
    string Username,
    string Email,
    string Password,
    string FullName,
    string EmployeeNo,
    string GroupCode);

public record UpdateUserRequest(
    string FullName,
    string Email,
    string EmployeeNo,
    string GroupCode,
    string? NewPassword);

[Authorize]
[ApiController]
[Route("api/v1/security")]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await mediator.Send(new GetUsersQuery(GetTenantId()), ct);
        return Ok(ApiResponse<IReadOnlyList<UserDto>>.Ok(result));
    }

    [HttpGet("groups")]
    public async Task<IActionResult> ListGroups(CancellationToken ct)
    {
        var result = await mediator.Send(new GetUserGroupsQuery(GetTenantId()), ct);
        return Ok(ApiResponse<IReadOnlyList<UserGroupDto>>.Ok(result));
    }

    [HttpPost("users")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest req, CancellationToken ct)
    {
        var id = await mediator.Send(new CreateUserCommand(
            GetTenantId(), req.Username, req.Email, req.Password,
            req.FullName, req.EmployeeNo, req.GroupCode), ct);
        return Ok(ApiResponse<object>.Ok(new { id }));
    }

    [HttpPut("users/{userId:guid}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateUserRequest req, CancellationToken ct)
    {
        var ok = await mediator.Send(new UpdateUserCommand(
            GetTenantId(), userId, req.FullName, req.Email,
            req.EmployeeNo, req.GroupCode, req.NewPassword), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    [HttpPatch("users/{userId:guid}/activate")]
    public async Task<IActionResult> Activate(Guid userId, CancellationToken ct)
    {
        var ok = await mediator.Send(new ToggleUserActiveCommand(GetTenantId(), userId, true), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    [HttpPatch("users/{userId:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid userId, CancellationToken ct)
    {
        var ok = await mediator.Send(new ToggleUserActiveCommand(GetTenantId(), userId, false), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
