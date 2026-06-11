using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowEvent : Entity
{
    private WorkflowEvent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FunctionName { get; private set; }
    public int TableId { get; private set; }
    public string Description { get; private set; }
    public int RequestPageId { get; private set; }
    public string DynamicReqPageEntityName { get; private set; }
    public bool UsedForRecordChange { get; private set; }
    public bool Independent { get; private set; }

    public static OperationResult<WorkflowEvent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowEvent, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowEvent()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowEvent, DomainError>.Ok(entity);
    }
}
