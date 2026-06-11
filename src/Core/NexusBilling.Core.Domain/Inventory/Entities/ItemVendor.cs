using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemVendor : Entity
{
    private ItemVendor() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VendorNo { get; private set; }
    public string LeadTimeCalculation { get; private set; }
    public string VendorItemNo { get; private set; }
    public string VariantCode { get; private set; }

    public static OperationResult<ItemVendor, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemVendor, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemVendor()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemVendor, DomainError>.Ok(entity);
    }
}
