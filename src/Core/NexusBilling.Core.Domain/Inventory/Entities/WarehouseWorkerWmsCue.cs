using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseWorkerWmsCue : Entity
{
    private WarehouseWorkerWmsCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<WarehouseWorkerWmsCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseWorkerWmsCue, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseWorkerWmsCue()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseWorkerWmsCue, DomainError>.Ok(entity);
    }
}
