using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowUserGroup : Entity
{
    private WorkflowUserGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<WorkflowUserGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowUserGroup, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowUserGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowUserGroup, DomainError>.Ok(entity);
    }
}
