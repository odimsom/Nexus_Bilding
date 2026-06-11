using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowUserGroupMember : Entity
{
    private WorkflowUserGroupMember() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorkflowUserGroupCode { get; private set; }
    public string UserName { get; private set; }
    public int SequenceNo { get; private set; }

    public static OperationResult<WorkflowUserGroupMember, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowUserGroupMember, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowUserGroupMember()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowUserGroupMember, DomainError>.Ok(entity);
    }
}
