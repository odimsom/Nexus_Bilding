using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowStepInstanceArchive : Entity
{
    private WorkflowStepInstanceArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public string WorkflowCode { get; private set; }
    public int WorkflowStepId { get; private set; }
    public string Description { get; private set; }
    public bool EntryPoint { get; private set; }
    public string RecordId { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public string CreatedByUserId { get; private set; }
    public DateTime? LastModifiedDateTime { get; private set; }
    public string LastModifiedByUserId { get; private set; }
    public short Status { get; private set; }
    public int PreviousWorkflowStepId { get; private set; }
    public int NextWorkflowStepId { get; private set; }
    public short Type { get; private set; }
    public string FunctionName { get; private set; }
    public Guid Argument { get; private set; }
    public string OriginalWorkflowCode { get; private set; }
    public int OriginalWorkflowStepId { get; private set; }
    public int SequenceNo { get; private set; }

    public static OperationResult<WorkflowStepInstanceArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowStepInstanceArchive, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowStepInstanceArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowStepInstanceArchive, DomainError>.Ok(entity);
    }
}
