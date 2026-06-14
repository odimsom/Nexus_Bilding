using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Api.Controllers.Administration;

[Authorize]
[ApiController]
[Route("api/v1/administration/payment-terms")]
public class PaymentTermsController(IPaymentTermsRepository repo) : ControllerBase
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

            // Always include CUOTAS if not present
            if (!mapped.Any(m => ((dynamic)m).code == "CUOTAS"))
                mapped.Add(new { code = "CUOTAS", description = "Cuotas" });

            return Ok(ApiResponse<object>.Ok(mapped));
        }
        catch
        {
            return Ok(ApiResponse<object>.Ok(Defaults));
        }
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
