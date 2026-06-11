using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class CapacityUnitOfMeasure : Entity
{
    private CapacityUnitOfMeasure() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short Type { get; private set; }

    public static OperationResult<CapacityUnitOfMeasure, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CapacityUnitOfMeasure, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CapacityUnitOfMeasure()
        {
            TenantId = tenantId
        };
        return OperationResult<CapacityUnitOfMeasure, DomainError>.Ok(entity);
    }
}
