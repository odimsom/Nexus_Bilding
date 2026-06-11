using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ColumnLayoutName : Entity
{
    private ColumnLayoutName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AnalysisViewName { get; private set; }

    public static OperationResult<ColumnLayoutName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ColumnLayoutName, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ColumnLayoutName()
        {
            TenantId = tenantId
        };
        return OperationResult<ColumnLayoutName, DomainError>.Ok(entity);
    }
}
