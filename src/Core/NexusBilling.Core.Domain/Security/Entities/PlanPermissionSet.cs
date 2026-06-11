using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class PlanPermissionSet : Entity
{
    private PlanPermissionSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid PlanId { get; private set; }
    public string PermissionSetId { get; private set; }

    public static OperationResult<PlanPermissionSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PlanPermissionSet, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PlanPermissionSet()
        {
            TenantId = tenantId
        };
        return OperationResult<PlanPermissionSet, DomainError>.Ok(entity);
    }
}
