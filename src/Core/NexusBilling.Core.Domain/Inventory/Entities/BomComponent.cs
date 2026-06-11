using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BomComponent : Entity
{
    private BomComponent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ParentItemNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityPer { get; private set; }
    public string Position { get; private set; }
    public string Position2 { get; private set; }
    public string Position3 { get; private set; }
    public string MachineNo { get; private set; }
    public string LeadTimeOffset { get; private set; }
    public short ResourceUsageType { get; private set; }
    public string VariantCode { get; private set; }
    public int InstalledInLineNo { get; private set; }
    public string InstalledInItemNo { get; private set; }

    public static OperationResult<BomComponent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BomComponent, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BomComponent()
        {
            TenantId = tenantId
        };
        return OperationResult<BomComponent, DomainError>.Ok(entity);
    }
}
