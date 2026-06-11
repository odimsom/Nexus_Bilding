using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemCrossReference : Entity
{
    private ItemCrossReference() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public short CrossReferenceType { get; private set; }
    public string CrossReferenceTypeNo { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string Description { get; private set; }
    public bool DiscontinueBarCode { get; private set; }

    public static OperationResult<ItemCrossReference, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemCrossReference, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemCrossReference()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemCrossReference, DomainError>.Ok(entity);
    }
}
