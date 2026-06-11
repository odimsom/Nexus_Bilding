using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class DynamicRequestPageField : Entity
{
    private DynamicRequestPageField() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }

    public static OperationResult<DynamicRequestPageField, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DynamicRequestPageField, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DynamicRequestPageField()
        {
            TenantId = tenantId
        };
        return OperationResult<DynamicRequestPageField, DomainError>.Ok(entity);
    }
}
