using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Api.Controllers.Administration;

public record PaymentTermsRequest(string Description, string? Code = null);

[Authorize]
[ApiController]
[Route("api/v1/administration/payment-terms")]
public class PaymentTermsController(IPaymentTermsRepository repo, IUnitOfWork uow) : ControllerBase
{
    private static readonly List<object> Defaults =
    [
        new { code = "CM",     description = "Contado" },
        new { code = "15D",    description = "15 Días" },
        new { code = "30D",    description = "30 Días" },
        new { code = "60D",    description = "60 Días" },
        new { code = "90D",    description = "90 Días" },
        new { code = "CUOTAS", description = "Cuotas" },
    ];

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var tid = TenantIdentifier.Create(tenantId);
            var items = await repo.FindAsync(p => p.TenantId == tid, cancellationToken: cancellationToken);

            if (!items.Any())
                return Ok(ApiResponse<object>.Ok(Defaults));

            var mapped = items
                .OrderBy(p => p.Code)
                .Select(p => new { code = p.Code, description = p.Description })
                .ToList<object>();

            if (!mapped.Any(m => ((dynamic)m).code == "CUOTAS"))
                mapped.Add(new { code = "CUOTAS", description = "Cuotas" });

            return Ok(ApiResponse<object>.Ok(mapped));
        }
        catch
        {
            return Ok(ApiResponse<object>.Ok(Defaults));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentTermsRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        if (string.IsNullOrWhiteSpace(req.Code))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "El código es obligatorio."));

        var existing = await repo.FindAsync(p => p.TenantId == tid && p.Code == req.Code, cancellationToken: ct);
        if (existing.Any())
            return Conflict(ApiResponse<object?>.Fail("CONFLICT", $"Ya existe una condición con código {req.Code}."));

        var result = PaymentTerms.Create(tid);
        if (!result.IsSuccess) return BadRequest(ApiResponse<object?>.Fail("VALIDATION", result.GetError()!.Message));

        var entity = result.GetValue()!;
        entity.Code = req.Code.ToUpperInvariant().Trim();
        entity.Description = req.Description.Trim();

        await repo.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, description = entity.Description }));
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] PaymentTermsRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(p => p.TenantId == tid && p.Code == code, cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null) return NotFound(ApiResponse<object?>.NotFound($"Condición {code} no encontrada."));

        entity.Description = req.Description.Trim();
        await repo.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object?>.Ok(null));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(p => p.TenantId == tid && p.Code == code, cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null) return NotFound(ApiResponse<object?>.NotFound($"Condición {code} no encontrada."));

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
