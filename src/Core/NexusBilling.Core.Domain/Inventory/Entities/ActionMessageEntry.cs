using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ActionMessageEntry : Entity
{
    private ActionMessageEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public short Type { get; private set; }
    public int ReservationEntry { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime? NewDate { get; private set; }
    public short Calculation { get; private set; }
    public bool SuppressedActionMsg { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceId { get; private set; }
    public string SourceBatchName { get; private set; }
    public int SourceProdOrderLine { get; private set; }
    public int SourceRefNo { get; private set; }
    public string LocationCode { get; private set; }
    public string BinCode { get; private set; }
    public string VariantCode { get; private set; }
    public string ItemNo { get; private set; }

    public static OperationResult<ActionMessageEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ActionMessageEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ActionMessageEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ActionMessageEntry, DomainError>.Ok(entity);
    }
}
