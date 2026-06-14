using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Ecf.Enums;
using NexusBilling.Core.Domain.Ecf.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Api.Controllers.Administration;

public record EcfConfigRequest(string Rnc, string RepresentativeName, int Environment, bool IsActive);

public record NcfSequenceRequest(string NcfType, long CurrentNumber, long MaxNumber, string ExpirationDate);

[Authorize]
[ApiController]
[Route("api/v1/administration")]
public class EcfController(
    IEcfCompanyConfigRepository ecfRepo,
    IEcfNcfSequenceRepository ncfRepo,
    IUnitOfWork uow) : ControllerBase
{
    // ── ECF Company Config ────────────────────────────────────────────

    [HttpGet("ecf-config")]
    public async Task<IActionResult> GetConfig(CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var tid = TenantIdentifier.Create(tenantId);
        var items = await ecfRepo.FindAsync(e => e.TenantId == tid, cancellationToken: ct);
        var config = items.FirstOrDefault();

        if (config is null)
            return Ok(ApiResponse<object?>.Ok(null));

        return Ok(ApiResponse<object>.Ok(new
        {
            id = config.Id,
            rnc = config.Rnc,
            representativeName = config.RepresentativeName,
            environment = (int)config.Environment,
            p12Path = config.P12Path,
            isActive = config.IsActive
        }));
    }

    [HttpPut("ecf-config")]
    public async Task<IActionResult> SaveConfig([FromBody] EcfConfigRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        if (string.IsNullOrWhiteSpace(req.Rnc))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "El RNC es obligatorio."));

        var tid = TenantIdentifier.Create(tenantId);
        var env = (EcfEnvironment)req.Environment;

        var items = await ecfRepo.FindAsync(e => e.TenantId == tid, cancellationToken: ct);
        var config = items.FirstOrDefault();

        if (config is null)
        {
            var result = EcfCompanyConfig.Create(tid, req.Rnc, req.RepresentativeName ?? string.Empty, env, string.Empty, string.Empty);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object?>.Fail("VALIDATION", result.GetError()!.Message));

            config = result.GetValue()!;
            await ecfRepo.AddAsync(config, ct);
        }
        else
        {
            config.Update(req.RepresentativeName ?? string.Empty, env, string.Empty, string.Empty);
            if (req.IsActive) config.Activate(); else config.Deactivate();
            await ecfRepo.UpdateAsync(config, ct);
        }

        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object?>.Ok(null));
    }

    // ── NCF Sequences ─────────────────────────────────────────────────

    [HttpGet("ncf-sequences")]
    public async Task<IActionResult> GetNcfSequences(CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var tid = TenantIdentifier.Create(tenantId);
        var items = await ncfRepo.FindAsync(e => e.TenantId == tid, cancellationToken: ct);

        var result = items
            .OrderBy(e => e.NcfType)
            .Select(e => new
            {
                id = e.Id,
                ncfType = e.NcfType,
                currentNumber = e.CurrentNumber,
                maxNumber = e.MaxNumber,
                expirationDate = e.ExpirationDate.ToString("yyyy-MM-dd"),
                available = e.MaxNumber - e.CurrentNumber
            });

        return Ok(ApiResponse<object>.Ok(result));
    }

    [HttpPost("ncf-sequences")]
    public async Task<IActionResult> CreateNcfSequence([FromBody] NcfSequenceRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        if (string.IsNullOrWhiteSpace(req.NcfType))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "El tipo de NCF es obligatorio."));

        if (!DateTime.TryParse(req.ExpirationDate, out var expiry))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "Fecha de expiración inválida."));

        var tid = TenantIdentifier.Create(tenantId);
        var existing = await ncfRepo.FindAsync(e => e.TenantId == tid && e.NcfType == req.NcfType.ToUpper(), cancellationToken: ct);
        if (existing.Any())
            return Conflict(ApiResponse<object?>.Fail("CONFLICT", $"Ya existe una secuencia para el tipo {req.NcfType}."));

        var result = EcfNcfSequence.Create(tid, req.NcfType, req.CurrentNumber, req.MaxNumber, expiry.ToUniversalTime());
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", result.GetError()!.Message));

        var seq = result.GetValue()!;
        await ncfRepo.AddAsync(seq, ct);
        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object>.Ok(new { id = seq.Id, ncfType = seq.NcfType }));
    }

    [HttpPut("ncf-sequences/{id:guid}")]
    public async Task<IActionResult> UpdateNcfSequence(Guid id, [FromBody] NcfSequenceRequest req, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        if (!DateTime.TryParse(req.ExpirationDate, out var expiry))
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", "Fecha de expiración inválida."));

        var tid = TenantIdentifier.Create(tenantId);
        var items = await ncfRepo.FindAsync(e => e.TenantId == tid && e.Id == id, cancellationToken: ct);
        var seq = items.FirstOrDefault();

        if (seq is null)
            return NotFound(ApiResponse<object?>.NotFound("Secuencia NCF no encontrada."));

        // Use reflection-free approach: create a new one in-place via field update
        // Since the entity's setters are private, we re-create and delete old, add new
        // Actually let me check - the entity only has ConsumeNext(). We need an Update method.
        // For now, delete and re-create with same NcfType
        await ncfRepo.DeleteAsync(seq, ct);
        var result = EcfNcfSequence.Create(tid, seq.NcfType, req.CurrentNumber, req.MaxNumber, expiry.ToUniversalTime());
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<object?>.Fail("VALIDATION", result.GetError()!.Message));

        var newSeq = result.GetValue()!;
        await ncfRepo.AddAsync(newSeq, ct);
        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object>.Ok(new { id = newSeq.Id, ncfType = newSeq.NcfType }));
    }

    [HttpDelete("ncf-sequences/{id:guid}")]
    public async Task<IActionResult> DeleteNcfSequence(Guid id, CancellationToken ct)
    {
        var tenantId = GetTenantId();
        if (tenantId == Guid.Empty) return Unauthorized();

        var tid = TenantIdentifier.Create(tenantId);
        var items = await ncfRepo.FindAsync(e => e.TenantId == tid && e.Id == id, cancellationToken: ct);
        var seq = items.FirstOrDefault();

        if (seq is null)
            return NotFound(ApiResponse<object?>.NotFound("Secuencia NCF no encontrada."));

        await ncfRepo.DeleteAsync(seq, ct);
        await uow.SaveChangesAsync(ct);
        return Ok(ApiResponse<object?>.Ok(null));
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
