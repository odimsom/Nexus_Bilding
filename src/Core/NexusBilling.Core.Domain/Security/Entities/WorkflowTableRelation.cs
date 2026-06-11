using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowTableRelation : Entity
{
    private WorkflowTableRelation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }
    public int RelatedTableId { get; private set; }
    public int RelatedFieldId { get; private set; }

    public static OperationResult<WorkflowTableRelation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowTableRelation, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowTableRelation()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowTableRelation, DomainError>.Ok(entity);
    }
}
