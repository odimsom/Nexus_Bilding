using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class AvgCostAdjmtEntryPoint : Entity
{
    private AvgCostAdjmtEntryPoint() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string LocationCode { get; private set; }
    public DateTime? ValuationDate { get; private set; }
    public bool CostIsAdjusted { get; private set; }

    public static OperationResult<AvgCostAdjmtEntryPoint, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AvgCostAdjmtEntryPoint, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AvgCostAdjmtEntryPoint()
        {
            TenantId = tenantId
        };
        return OperationResult<AvgCostAdjmtEntryPoint, DomainError>.Ok(entity);
    }
}
