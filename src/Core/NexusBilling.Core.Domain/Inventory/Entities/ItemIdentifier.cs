using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemIdentifier : Entity
{
    private ItemIdentifier() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }

    public static OperationResult<ItemIdentifier, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemIdentifier, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemIdentifier()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemIdentifier, DomainError>.Ok(entity);
    }
}
