using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class InventoryPostingSetup : Entity
{
    private InventoryPostingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string LocationCode { get; private set; }
    public string InvtPostingGroupCode { get; private set; }
    public string InventoryAccount { get; private set; }
    public string InventoryAccountInterim { get; private set; }
    public string WipAccount { get; private set; }
    public string MaterialVarianceAccount { get; private set; }
    public string CapacityVarianceAccount { get; private set; }
    public string MfgOverheadVarianceAccount { get; private set; }
    public string CapOverheadVarianceAccount { get; private set; }
    public string SubcontractedVarianceAccount { get; private set; }

    public static OperationResult<InventoryPostingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InventoryPostingSetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InventoryPostingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<InventoryPostingSetup, DomainError>.Ok(entity);
    }
}
