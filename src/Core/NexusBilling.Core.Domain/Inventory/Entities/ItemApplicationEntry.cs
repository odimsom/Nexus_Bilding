using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemApplicationEntry : Entity
{
    private ItemApplicationEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int ItemLedgerEntryNo { get; private set; }
    public int InboundItemEntryNo { get; private set; }
    public int OutboundItemEntryNo { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public int TransferredFromEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string CreatedByUser { get; private set; }
    public DateTime? LastModifiedDate { get; private set; }
    public string LastModifiedByUser { get; private set; }
    public bool CostApplication { get; private set; }
    public DateTime? OutputCompletelyInvdDate { get; private set; }
    public bool OutboundEntryIsUpdated { get; private set; }

    public static OperationResult<ItemApplicationEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemApplicationEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemApplicationEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemApplicationEntry, DomainError>.Ok(entity);
    }
}
