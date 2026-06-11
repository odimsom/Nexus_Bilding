using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class WorkType : Entity
{
    private WorkType() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string UnitOfMeasureCode { get; private set; }

    public static OperationResult<WorkType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkType, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkType()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkType, DomainError>.Ok(entity);
    }
}
