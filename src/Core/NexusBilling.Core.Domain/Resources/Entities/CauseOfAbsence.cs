using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class CauseOfAbsence : Entity
{
    private CauseOfAbsence() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string UnitOfMeasureCode { get; private set; }

    public static OperationResult<CauseOfAbsence, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CauseOfAbsence, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CauseOfAbsence()
        {
            TenantId = tenantId
        };
        return OperationResult<CauseOfAbsence, DomainError>.Ok(entity);
    }
}
