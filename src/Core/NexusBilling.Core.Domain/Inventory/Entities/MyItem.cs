using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class MyItem : Entity
{
    private MyItem() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string ItemNo { get; private set; }
    public string Description { get; private set; }
    public decimal UnitPrice { get; private set; }

    public static OperationResult<MyItem, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MyItem, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MyItem()
        {
            TenantId = tenantId
        };
        return OperationResult<MyItem, DomainError>.Ok(entity);
    }
}
