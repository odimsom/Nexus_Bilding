using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserGroupPlan : Entity
{
    private UserGroupPlan() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid PlanId { get; private set; }
    public string UserGroupCode { get; private set; }

    public static OperationResult<UserGroupPlan, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserGroupPlan, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserGroupPlan()
        {
            TenantId = tenantId
        };
        return OperationResult<UserGroupPlan, DomainError>.Ok(entity);
    }
}
