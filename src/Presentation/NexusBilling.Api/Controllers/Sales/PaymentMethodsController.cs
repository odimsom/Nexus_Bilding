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
[Route("api/v1/sales/payment-methods")]
public class PaymentMethodsController(IPaymentMethodRepository repo, IUnitOfWork uow) : ControllerBase
{
    private static readonly List<object> Defaults =
    [
        new { code = "EFE",   description = "Efectivo"              },
        new { code = "TRF",   description = "Transferencia Bancaria"},
        new { code = "CHQ",   description = "Cheque"                },
        new { code = "TC",    description = "Tarjeta de Crédito"    },
        new { code = "TD",    description = "Tarjeta de Débito"     },
        new { code = "CRED",  description = "Crédito"               },
    ];

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        try
        {
            var tid = TenantIdentifier.Create(tenantId);
            var items = await repo.FindAsync(p => p.TenantId == tid, cancellationToken: ct);

            if (!items.Any())
                return Ok(ApiResponse<object>.Ok(Defaults));

            return Ok(ApiResponse<object>.Ok(
                items.OrderBy(p => p.Code)
                     .Select(p => new { code = p.Code, description = p.Description })
                     .ToList<object>()
            ));
        }
        catch
        {
            return Ok(ApiResponse<object>.Ok(Defaults));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentMethodRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Code) || string.IsNullOrWhiteSpace(req.Description))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "Código y descripción son obligatorios."));

        var tid = TenantIdentifier.Create(tenantId);
        var existing = await repo.FindAsync(p => p.TenantId == tid && p.Code == req.Code.Trim().ToUpper(), cancellationToken: ct);
        if (existing.Any())
            return Conflict(ApiResponse<object?>.Fail("DUPLICATE", $"Ya existe un método de pago con código '{req.Code}'."));

        var result = PaymentMethod.Create(tid);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail("DOMAIN", result.GetError()!.Message));

        var entity = result.GetValue()!;
        entity.Code = req.Code.Trim().ToUpper();
        entity.Description = req.Description.Trim();

        await repo.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, description = entity.Description }));
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] PaymentMethodRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Description))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "La descripción es obligatoria."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(p => p.TenantId == tid && p.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Método de pago no encontrado."));

        entity.Description = req.Description.Trim();
        await repo.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, description = entity.Description }));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(p => p.TenantId == tid && p.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Método de pago no encontrado."));

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

public record PaymentMethodRequest(string Code, string Description);
