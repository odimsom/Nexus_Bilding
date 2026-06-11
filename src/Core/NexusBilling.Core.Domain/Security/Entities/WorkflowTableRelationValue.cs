using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowTableRelationValue : Entity
{
    private WorkflowTableRelationValue() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid WorkflowStepInstanceId { get; private set; }
    public string WorkflowCode { get; private set; }
    public int WorkflowStepId { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }
    public int RelatedTableId { get; private set; }
    public int RelatedFieldId { get; private set; }
    public string Value { get; private set; }
    public string RecordId { get; private set; }

    public static OperationResult<WorkflowTableRelationValue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowTableRelationValue, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowTableRelationValue()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowTableRelationValue, DomainError>.Ok(entity);
    }
}
