using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemEntryRelation : Entity
{
    private ItemEntryRelation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ItemEntryNo { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceId { get; private set; }
    public string SourceBatchName { get; private set; }
    public int SourceProdOrderLine { get; private set; }
    public int SourceRefNo { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }

    public static OperationResult<ItemEntryRelation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemEntryRelation, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemEntryRelation()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemEntryRelation, DomainError>.Ok(entity);
    }
}
