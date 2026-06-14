using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Api.Controllers.Sales;

[Authorize]
[ApiController]
[Route("api/v1/sales/salespersons")]
public class SalespersonsController(ISalespersonPurchaserRepository repo, IUnitOfWork uow) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var tid = TenantIdentifier.Create(tenantId);
            var items = await repo.FindAsync(s => s.TenantId == tid, cancellationToken: ct);

            return Ok(ApiResponse<object>.Ok(
                items.OrderBy(s => s.Code)
                     .Select(s => new { code = s.Code, name = s.Name, email = s.EMail, phone = s.PhoneNo, jobTitle = s.JobTitle })
                     .ToList<object>()
            ));
        }
        catch
        {
            return Ok(ApiResponse<object>.Ok(new List<object>()));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalespersonRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Code) || string.IsNullOrWhiteSpace(req.Name))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "Código y nombre son obligatorios."));

        var tid = TenantIdentifier.Create(tenantId);
        var existing = await repo.FindAsync(s => s.TenantId == tid && s.Code == req.Code.Trim().ToUpper(), cancellationToken: ct);
        if (existing.Any())
            return Conflict(ApiResponse<object?>.Fail("DUPLICATE", $"Ya existe un vendedor con código '{req.Code}'."));

        var result = SalespersonPurchaser.Create(tid);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail("DOMAIN", result.GetError()!.Message));

        var entity = result.GetValue()!;
        entity.Code = req.Code.Trim().ToUpper();
        entity.Name = req.Name.Trim();
        entity.EMail = req.Email?.Trim() ?? string.Empty;
        entity.PhoneNo = req.Phone?.Trim() ?? string.Empty;
        entity.JobTitle = req.JobTitle?.Trim() ?? string.Empty;

        await repo.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, name = entity.Name, email = entity.EMail, phone = entity.PhoneNo, jobTitle = entity.JobTitle }));
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] SalespersonRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Name))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "El nombre es obligatorio."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(s => s.TenantId == tid && s.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Vendedor no encontrado."));

        entity.Name = req.Name.Trim();
        entity.EMail = req.Email?.Trim() ?? entity.EMail;
        entity.PhoneNo = req.Phone?.Trim() ?? entity.PhoneNo;
        entity.JobTitle = req.JobTitle?.Trim() ?? entity.JobTitle;

        await repo.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, name = entity.Name, email = entity.EMail, phone = entity.PhoneNo, jobTitle = entity.JobTitle }));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(s => s.TenantId == tid && s.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Vendedor no encontrado."));

        await repo.DeleteAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object?>.Ok(null));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}

public record SalespersonRequest(string Code, string Name, string? Email, string? Phone, string? JobTitle);
