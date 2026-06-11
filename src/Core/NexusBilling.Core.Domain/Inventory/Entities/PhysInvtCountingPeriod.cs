using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PhysInvtCountingPeriod : Entity
{
    private PhysInvtCountingPeriod() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public int CountFrequencyPerYear { get; private set; }

    public static OperationResult<PhysInvtCountingPeriod, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PhysInvtCountingPeriod, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PhysInvtCountingPeriod()
        {
            TenantId = tenantId
        };
        return OperationResult<PhysInvtCountingPeriod, DomainError>.Ok(entity);
    }
}
