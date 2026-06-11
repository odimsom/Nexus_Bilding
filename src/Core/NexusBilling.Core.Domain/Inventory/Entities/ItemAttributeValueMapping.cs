using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemAttributeValueMapping : Entity
{
    private ItemAttributeValueMapping() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public string No { get; private set; }
    public int ItemAttributeId { get; private set; }
    public int ItemAttributeValueId { get; private set; }

    public static OperationResult<ItemAttributeValueMapping, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemAttributeValueMapping, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemAttributeValueMapping()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemAttributeValueMapping, DomainError>.Ok(entity);
    }
}
