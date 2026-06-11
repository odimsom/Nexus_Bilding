using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemJournalLine : Entity
{
    private ItemJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public int LineNo { get; private set; }
    public string ItemNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short EntryType { get; private set; }
    public string SourceNo { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public string SourcePostingGroup { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal InvoicedQuantity { get; private set; }
    public decimal UnitAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal Amount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string SourceCode { get; private set; }
    public int AppliesToEntry { get; private set; }
    public int ItemShptEntryNo { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public decimal IndirectCost { get; private set; }
    public short SourceType { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short RecurringMethod { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string RecurringFrequency { get; private set; }
    public bool DropShipment { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string NewLocationCode { get; private set; }
    public string NewShortcutDimension1Code { get; private set; }
    public string NewShortcutDimension2Code { get; private set; }
    public decimal QtyCalculated { get; private set; }
    public decimal QtyPhysInventory { get; private set; }
    public int LastItemLedgerEntryNo { get; private set; }
    public bool PhysInventory { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string EntryExitPoint { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string PostingNoSeries { get; private set; }
    public decimal UnitCostAcy { get; private set; }
    public string SourceCurrencyCode { get; private set; }
    public short DocumentType { get; private set; }
    public int DocumentLineNo { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public int NewDimensionSetId { get; private set; }
    public bool AssembleToOrder { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public bool JobPurchase { get; private set; }
    public int JobContractEntryNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string NewBinCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public bool DerivedFromBlanketOrder { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal InvoicedQtyBase { get; private set; }
    public int Level { get; private set; }
    public short FlushingMethod { get; private set; }
    public bool ChangedByUser { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string OriginallyOrderedNo { get; private set; }
    public string OriginallyOrderedVarCode { get; private set; }
    public bool OutOfStockSubstitution { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public DateTime? PlannedDeliveryDate { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public short ValueEntryType { get; private set; }
    public string ItemChargeNo { get; private set; }
    public decimal InventoryValueCalculated { get; private set; }
    public decimal InventoryValueRevalued { get; private set; }
    public short VarianceType { get; private set; }
    public short InventoryValuePer { get; private set; }
    public bool PartialRevaluation { get; private set; }
    public int AppliesFromEntry { get; private set; }
    public string InvoiceNo { get; private set; }
    public decimal UnitCostCalculated { get; private set; }
    public decimal UnitCostRevalued { get; private set; }
    public decimal AppliedAmount { get; private set; }
    public bool UpdateStandardCost { get; private set; }
    public decimal AmountAcy { get; private set; }
    public bool Correction { get; private set; }
    public bool Adjustment { get; private set; }
    public int AppliesToValueEntry { get; private set; }
    public string InvoiceToSourceNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string OperationNo { get; private set; }
    public string WorkCenterNo { get; private set; }
    public decimal SetupTime { get; private set; }
    public decimal RunTime { get; private set; }
    public decimal StopTime { get; private set; }
    public decimal OutputQuantity { get; private set; }
    public decimal ScrapQuantity { get; private set; }
    public decimal ConcurrentCapacity { get; private set; }
    public decimal SetupTimeBase { get; private set; }
    public decimal RunTimeBase { get; private set; }
    public decimal StopTimeBase { get; private set; }
    public decimal OutputQuantityBase { get; private set; }
    public decimal ScrapQuantityBase { get; private set; }
    public string CapUnitOfMeasureCode { get; private set; }
    public decimal QtyPerCapUnitOfMeasure { get; private set; }
    public string StartingTime { get; private set; }
    public string EndingTime { get; private set; }
    public string RoutingNo { get; private set; }
    public int RoutingReferenceNo { get; private set; }
    public int ProdOrderCompLineNo { get; private set; }
    public bool Finished { get; private set; }
    public short UnitCostCalculation { get; private set; }
    public bool Subcontracting { get; private set; }
    public string StopCode { get; private set; }
    public string ScrapCode { get; private set; }
    public string WorkCenterGroupCode { get; private set; }
    public string WorkShiftCode { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public string NewSerialNo { get; private set; }
    public string NewLotNo { get; private set; }
    public DateTime? NewItemExpirationDate { get; private set; }
    public DateTime? ItemExpirationDate { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public bool WarehouseAdjustment { get; private set; }
    public string PhysInvtCountingPeriodCode { get; private set; }
    public short PhysInvtCountingPeriodType { get; private set; }
    public decimal OverheadRate { get; private set; }
    public decimal SingleLevelMaterialCost { get; private set; }
    public decimal SingleLevelCapacityCost { get; private set; }
    public decimal SingleLevelSubcontrdCost { get; private set; }
    public decimal SingleLevelCapOvhdCost { get; private set; }
    public decimal SingleLevelMfgOvhdCost { get; private set; }
    public decimal RolledUpMaterialCost { get; private set; }
    public decimal RolledUpCapacityCost { get; private set; }
    public decimal RolledUpSubcontractedCost { get; private set; }
    public decimal RolledUpMfgOvhdCost { get; private set; }
    public decimal RolledUpCapOverheadCost { get; private set; }

    public static OperationResult<ItemJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemJournalLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemJournalLine, DomainError>.Ok(entity);
    }
}
