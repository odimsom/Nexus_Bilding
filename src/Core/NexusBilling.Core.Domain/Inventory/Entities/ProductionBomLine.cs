using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProductionBomLine : Entity
{
    private ProductionBomLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ProductionBomNo { get; private set; }
    public int LineNo { get; private set; }
    public string VersionCode { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal Quantity { get; private set; }
    public string Position { get; private set; }
    public string Position2 { get; private set; }
    public string Position3 { get; private set; }
    public string LeadTimeOffset { get; private set; }
    public string RoutingLinkCode { get; private set; }
    public decimal Scrap { get; private set; }
    public string VariantCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public decimal Length { get; private set; }
    public decimal Width { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Depth { get; private set; }
    public short CalculationFormula { get; private set; }
    public decimal QuantityPer { get; private set; }

    public static OperationResult<ProductionBomLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProductionBomLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProductionBomLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ProductionBomLine, DomainError>.Ok(entity);
    }
}
