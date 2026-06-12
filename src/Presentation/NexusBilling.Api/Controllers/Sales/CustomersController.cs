using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Sales.Commands;
using NexusBilling.Core.Application.Sales.Queries;

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
