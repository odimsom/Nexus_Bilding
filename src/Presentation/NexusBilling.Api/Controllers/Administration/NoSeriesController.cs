using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Administration.Services;
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

namespace NexusBilling.Api.Controllers.Administration;

public record UpsertNoSeriesRequest(string Code, string Description, bool DefaultNos, bool ManualNos);
public record UpsertNoSeriesLineRequest(string SeriesCode, string StartingNo, string EndingNo, int IncrementByNo);

[Authorize]
[ApiController]
[Route("api/v1/administration/no-series")]
public sealed class NoSeriesController(IMediator mediator, NoSeriesService noSeriesService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(new GetNoSeriesQuery(tenantId), cancellationToken);
        return Ok(ApiResponse<object>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] UpsertNoSeriesRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(
            new UpsertNoSeriesCommand(tenantId, req.Code, req.Description, req.DefaultNos, req.ManualNos),
            cancellationToken);

        return ok ? Ok(ApiResponse<object>.Ok(new { code = req.Code }))
                  : BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", "No se pudo guardar la serie."));
    }

    [HttpPost("lines")]
    public async Task<IActionResult> UpsertLine([FromBody] UpsertNoSeriesLineRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(
            new UpsertNoSeriesLineCommand(tenantId, req.SeriesCode, req.StartingNo, req.EndingNo, req.IncrementByNo),
            cancellationToken);

        return ok ? Ok(ApiResponse<object>.Ok(new { seriesCode = req.SeriesCode }))
                  : BadRequest(ApiResponse<object?>.Fail("BAD_REQUEST", "No se pudo guardar la línea."));
    }

    [HttpGet("{code}/next")]
    public async Task<IActionResult> PeekNext(string code, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var nextNo = await noSeriesService.PeekNextNoAsync(tenantId, code, cancellationToken);
        return nextNo is not null
            ? Ok(ApiResponse<object>.Ok(new { code, nextNo }))
            : NotFound(ApiResponse<object?>.NotFound($"La serie '{code}' no tiene línea activa configurada."));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var ok = await mediator.Send(new DeleteNoSeriesCommand(tenantId, code), cancellationToken);
        return ok ? Ok(ApiResponse<object>.Ok(new { code }))
                  : NotFound(ApiResponse<object?>.NotFound($"La serie {code} no fue encontrada."));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
