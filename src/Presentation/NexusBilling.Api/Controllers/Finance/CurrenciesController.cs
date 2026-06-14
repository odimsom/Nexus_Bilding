using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Api.Controllers.Finance;

[Authorize]
[ApiController]
[Route("api/v1/finance/currencies")]
public class CurrenciesController(ICurrencyRepository repo, IUnitOfWork uow) : ControllerBase
{
    private static readonly List<object> Defaults =
    [
        new { code = "DOP", description = "Peso Dominicano",  symbol = "RD$" },
        new { code = "USD", description = "Dólar Americano",  symbol = "$"   },
        new { code = "EUR", description = "Euro",             symbol = "€"   },
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
            var items = await repo.FindAsync(c => c.TenantId == tid, cancellationToken: ct);

            if (!items.Any())
                return Ok(ApiResponse<object>.Ok(Defaults));

            return Ok(ApiResponse<object>.Ok(
                items.OrderBy(c => c.Code)
                     .Select(c => new { code = c.Code, description = c.Description, symbol = c.Symbol ?? c.Code })
                     .ToList<object>()
            ));
        }
        catch
        {
            return Ok(ApiResponse<object>.Ok(Defaults));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CurrencyRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Code) || string.IsNullOrWhiteSpace(req.Description))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "Código y descripción son obligatorios."));

        var tid = TenantIdentifier.Create(tenantId);
        var existing = await repo.FindAsync(c => c.TenantId == tid && c.Code == req.Code.Trim().ToUpper(), cancellationToken: ct);
        if (existing.Any())
            return Conflict(ApiResponse<object?>.Fail("DUPLICATE", $"Ya existe una moneda con código '{req.Code}'."));

        var result = Currency.Create(tid);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail("DOMAIN", result.GetError()!.Message));

        var entity = result.GetValue()!;
        entity.Code = req.Code.Trim().ToUpper();
        entity.Description = req.Description.Trim();
        entity.Symbol = req.Symbol?.Trim() ?? req.Code.Trim().ToUpper();

        await repo.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, description = entity.Description, symbol = entity.Symbol }));
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] CurrencyRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        if (string.IsNullOrWhiteSpace(req.Description))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "La descripción es obligatoria."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(c => c.TenantId == tid && c.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Moneda no encontrada."));

        entity.Description = req.Description.Trim();
        if (!string.IsNullOrWhiteSpace(req.Symbol))
            entity.Symbol = req.Symbol.Trim();

        await repo.UpdateAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return Ok(ApiResponse<object>.Ok(new { code = entity.Code, description = entity.Description, symbol = entity.Symbol }));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty)
            return Unauthorized(ApiResponse<object?>.Fail("UNAUTHORIZED", "Token inválido."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await repo.FindAsync(c => c.TenantId == tid && c.Code == code.ToUpper(), cancellationToken: ct);
        var entity = items.FirstOrDefault();
        if (entity is null)
            return NotFound(ApiResponse<object?>.Fail("NOT_FOUND", "Moneda no encontrada."));

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

public record CurrencyRequest(string Code, string Description, string? Symbol);
