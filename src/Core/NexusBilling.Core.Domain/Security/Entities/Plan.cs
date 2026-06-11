using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class Plan : Entity
{
    private Plan() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid PlanId { get; private set; }
    public string Name { get; private set; }
    public int RoleCenterId { get; private set; }

    public static OperationResult<Plan, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Plan, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Plan()
        {
            TenantId = tenantId
        };
        return OperationResult<Plan, DomainError>.Ok(entity);
    }
}
