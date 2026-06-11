using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class StandardItemJournalLine : Entity
{
    private StandardItemJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public int LineNo { get; private set; }
    public string ItemNo { get; private set; }
    public short EntryType { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal Amount { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string SourceCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public decimal IndirectCost { get; private set; }
    public string StandardJournalCode { get; private set; }
    public string ReasonCode { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string CountryRegionCode { get; private set; }
    public decimal QtyCalculated { get; private set; }
    public decimal QtyPhysInventory { get; private set; }
    public bool PhysInventory { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string EntryExitPoint { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string PostingNoSeries { get; private set; }
    public int DimensionSetId { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public string OriginallyOrderedNo { get; private set; }
    public string OriginallyOrderedVarCode { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public short ValueEntryType { get; private set; }
    public string ItemChargeNo { get; private set; }
    public bool Correction { get; private set; }
    public string WorkCenterNo { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public decimal OverheadRate { get; private set; }

    public static OperationResult<StandardItemJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardItemJournalLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardItemJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardItemJournalLine, DomainError>.Ok(entity);
    }
}
