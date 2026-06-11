using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowStep : Entity
{
    private WorkflowStep() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public string WorkflowCode { get; private set; }
    public string Description { get; private set; }
    public bool EntryPoint { get; private set; }
    public int PreviousWorkflowStepId { get; private set; }
    public int NextWorkflowStepId { get; private set; }
    public short Type { get; private set; }
    public string FunctionName { get; private set; }
    public Guid Argument { get; private set; }
    public int SequenceNo { get; private set; }

    public static OperationResult<WorkflowStep, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowStep, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowStep()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowStep, DomainError>.Ok(entity);
    }
}
