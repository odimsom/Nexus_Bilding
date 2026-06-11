using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ValueEntry : Entity
{
    private ValueEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short ItemLedgerEntryType { get; private set; }
    public string SourceNo { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public string SourcePostingGroup { get; private set; }
    public int ItemLedgerEntryNo { get; private set; }
    public decimal ValuedQuantity { get; private set; }
    public decimal ItemLedgerEntryQuantity { get; private set; }
    public decimal InvoicedQuantity { get; private set; }
    public decimal CostPerUnit { get; private set; }
    public decimal SalesAmountActual { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public int AppliesToEntry { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public short SourceType { get; private set; }
    public decimal CostAmountActual { get; private set; }
    public decimal CostPostedToGL { get; private set; }
    public string ReasonCode { get; private set; }
    public bool DropShipment { get; private set; }
    public string JournalBatchName { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public decimal CostAmountActualAcy { get; private set; }
    public decimal CostPostedToGLAcy { get; private set; }
    public decimal CostPerUnitAcy { get; private set; }
    public short DocumentType { get; private set; }
    public int DocumentLineNo { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public bool ExpectedCost { get; private set; }
    public string ItemChargeNo { get; private set; }
    public bool ValuedByAverageCost { get; private set; }
    public bool PartialRevaluation { get; private set; }
    public bool Inventoriable { get; private set; }
    public DateTime? ValuationDate { get; private set; }
    public short EntryType { get; private set; }
    public short VarianceType { get; private set; }
    public decimal PurchaseAmountActual { get; private set; }
    public decimal PurchaseAmountExpected { get; private set; }
    public decimal SalesAmountExpected { get; private set; }
    public decimal CostAmountExpected { get; private set; }
    public decimal CostAmountNonInvtbl { get; private set; }
    public decimal CostAmountExpectedAcy { get; private set; }
    public decimal CostAmountNonInvtblAcy { get; private set; }
    public decimal ExpectedCostPostedToGL { get; private set; }
    public decimal ExpCostPostedToGLAcy { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public int JobLedgerEntryNo { get; private set; }
    public string VariantCode { get; private set; }
    public bool Adjustment { get; private set; }
    public bool AverageCostException { get; private set; }
    public int CapacityLedgerEntryNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string ReturnReasonCode { get; private set; }

    public static OperationResult<ValueEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ValueEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ValueEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ValueEntry, DomainError>.Ok(entity);
    }
}
