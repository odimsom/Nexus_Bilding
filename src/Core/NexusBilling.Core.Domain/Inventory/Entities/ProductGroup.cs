using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProductGroup : Entity
{
    private ProductGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string WarehouseClassCode { get; private set; }

    public static OperationResult<ProductGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProductGroup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProductGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<ProductGroup, DomainError>.Ok(entity);
    }
}
