using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemAttributeValue : Entity
{
    private ItemAttributeValue() { }

    public TenantIdentifier TenantId { get; private set; }
    public int AttributeId { get; private set; }
    public int IdNav { get; private set; }
    public string Value { get; private set; }
    public decimal NumericValue { get; private set; }
    public bool Blocked { get; private set; }

    public static OperationResult<ItemAttributeValue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemAttributeValue, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemAttributeValue()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemAttributeValue, DomainError>.Ok(entity);
    }
}
