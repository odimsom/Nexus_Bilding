using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Purchasing.Commands;
using NexusBilling.Core.Application.Purchasing.Queries;

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

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
