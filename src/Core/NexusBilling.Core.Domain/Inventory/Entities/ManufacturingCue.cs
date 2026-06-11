using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ManufacturingCue : Entity
{
    private ManufacturingCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<ManufacturingCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ManufacturingCue, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ManufacturingCue()
        {
            TenantId = tenantId
        };
        return OperationResult<ManufacturingCue, DomainError>.Ok(entity);
    }
}
