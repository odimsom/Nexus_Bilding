using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemCategory : Entity
{
    private ItemCategory() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string ParentCategory { get; private set; }
    public string Description { get; private set; }
    public int Indentation { get; private set; }
    public int PresentationOrder { get; private set; }
    public bool HasChildren { get; private set; }

    public static OperationResult<ItemCategory, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemCategory, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemCategory()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemCategory, DomainError>.Ok(entity);
    }
}
