using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Inventory.Features.Locations.Commands.UpsertLocation;
using NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocationByCode;
using NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocations;

namespace NexusBilling.Api.Controllers.Inventory;

public record UpsertLocationRequest(
    string Code,
    string Name,
    string Address,
    string City
);

[Authorize]
[ApiController]
[Route("api/v1/inventory/locations")]
public sealed class LocationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? search,
        CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var result = await mediator.Send(
            new GetLocationsQuery(tenantId, search),
            cancellationToken);

        return Ok(ApiResponse<object>.Ok(result));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var location = await mediator.Send(new GetLocationByCodeQuery(tenantId, code), cancellationToken);

        if (location is null)
            return NotFound(ApiResponse<object?>.NotFound($"El almacén {code} no fue encontrado."));

        return Ok(ApiResponse<object>.Ok(location));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertLocationRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new UpsertLocationCommand(tenantId, null, req.Code, req.Name, req.Address, req.City);

        var result = await mediator.Send(cmd, cancellationToken);
        return result.Created
            ? CreatedAtAction(nameof(GetByCode), new { code = result.Code },
                ApiResponse<object>.Ok(new { code = result.Code }))
            : Conflict(ApiResponse<object?>.Fail("CONFLICT", $"El almacén {req.Code} ya existe."));
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] UpsertLocationRequest req, CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var cmd = new UpsertLocationCommand(tenantId, code, req.Code, req.Name, req.Address, req.City);

        var result = await mediator.Send(cmd, cancellationToken);
        return result.Created
            ? NotFound(ApiResponse<object?>.NotFound($"El almacén {code} no fue encontrado."))
            : Ok(ApiResponse<object>.Ok(new { code = result.Code }));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
