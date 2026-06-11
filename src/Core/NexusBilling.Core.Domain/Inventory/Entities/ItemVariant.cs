using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemVariant : Entity
{
    private ItemVariant() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string ItemNo { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }

    public static OperationResult<ItemVariant, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemVariant, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemVariant()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemVariant, DomainError>.Ok(entity);
    }
}
