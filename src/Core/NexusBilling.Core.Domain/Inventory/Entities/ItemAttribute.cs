using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemAttribute : Entity
{
    private ItemAttribute() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public string Name { get; private set; }
    public bool Blocked { get; private set; }
    public short Type { get; private set; }
    public string UnitOfMeasure { get; private set; }

    public static OperationResult<ItemAttribute, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemAttribute, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemAttribute()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemAttribute, DomainError>.Ok(entity);
    }
}
