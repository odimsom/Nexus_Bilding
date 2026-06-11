using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowRule : Entity
{
    private WorkflowRule() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public int TableId { get; private set; }
    public int FieldNo { get; private set; }
    public short Operator { get; private set; }
    public string WorkflowCode { get; private set; }
    public int WorkflowStepId { get; private set; }
    public Guid WorkflowStepInstanceId { get; private set; }

    public static OperationResult<WorkflowRule, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowRule, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowRule()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowRule, DomainError>.Ok(entity);
    }
}
