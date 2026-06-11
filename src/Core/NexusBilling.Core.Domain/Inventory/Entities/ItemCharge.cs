using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemCharge : Entity
{
    private ItemCharge() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string SearchDescription { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }

    public static OperationResult<ItemCharge, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemCharge, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemCharge()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemCharge, DomainError>.Ok(entity);
    }
}
