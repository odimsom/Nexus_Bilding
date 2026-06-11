using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class InventoryPeriod : Entity
{
    private InventoryPeriod() { }

    public TenantIdentifier TenantId { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string Name { get; private set; }
    public bool Closed { get; private set; }

    public static OperationResult<InventoryPeriod, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InventoryPeriod, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InventoryPeriod()
        {
            TenantId = tenantId
        };
        return OperationResult<InventoryPeriod, DomainError>.Ok(entity);
    }
}
