using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ValueEntryRelation : Entity
{
    private ValueEntryRelation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ValueEntryNo { get; private set; }
    public string SourceRowid { get; private set; }

    public static OperationResult<ValueEntryRelation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ValueEntryRelation, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ValueEntryRelation()
        {
            TenantId = tenantId
        };
        return OperationResult<ValueEntryRelation, DomainError>.Ok(entity);
    }
}
