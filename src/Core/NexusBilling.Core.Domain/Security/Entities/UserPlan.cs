using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserPlan : Entity
{
    private UserPlan() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public Guid PlanId { get; private set; }

    public static OperationResult<UserPlan, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserPlan, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserPlan()
        {
            TenantId = tenantId
        };
        return OperationResult<UserPlan, DomainError>.Ok(entity);
    }
}
