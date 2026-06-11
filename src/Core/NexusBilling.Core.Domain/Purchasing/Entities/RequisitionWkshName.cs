using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class RequisitionWkshName : Entity
{
    private RequisitionWkshName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorksheetTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<RequisitionWkshName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RequisitionWkshName, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RequisitionWkshName()
        {
            TenantId = tenantId
        };
        return OperationResult<RequisitionWkshName, DomainError>.Ok(entity);
    }
}
