using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PhysInventoryLedgerEntry : Entity
{
    private PhysInventoryLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short EntryType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal Amount { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public decimal QtyCalculated { get; private set; }
    public decimal QtyPhysInventory { get; private set; }
    public int LastItemLedgerEntryNo { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string NoSeries { get; private set; }
    public int DimensionSetId { get; private set; }
    public string VariantCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string PhysInvtCountingPeriodCode { get; private set; }
    public short PhysInvtCountingPeriodType { get; private set; }

    public static OperationResult<PhysInventoryLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PhysInventoryLedgerEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PhysInventoryLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<PhysInventoryLedgerEntry, DomainError>.Ok(entity);
    }
}
