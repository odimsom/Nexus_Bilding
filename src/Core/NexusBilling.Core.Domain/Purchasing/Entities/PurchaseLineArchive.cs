using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchaseLineArchive : Entity
{
    private PurchaseLineArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string BuyFromVendorNo { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal OutstandingQuantity { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public decimal QtyToReceive { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
    public decimal QuantityDisc { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountIncludingVat { get; private set; }
    public decimal UnitPriceLcy { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public decimal UnitVolume { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string JobNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public decimal QtyRcdNotInvoiced { get; private set; }
    public decimal AmtRcdNotInvoiced { get; private set; }
    public decimal QuantityReceived { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ReceiptNo { get; private set; }
    public int ReceiptLineNo { get; private set; }
    public decimal Profit { get; private set; }
    public string PayToVendorNo { get; private set; }
    public decimal InvDiscountAmount { get; private set; }
    public string VendorItemNo { get; private set; }
    public string SalesOrderNo { get; private set; }
    public int SalesOrderLineNo { get; private set; }
    public bool DropShipment { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public short VatCalculationType { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public int AttachedToLineNo { get; private set; }
    public string EntryPoint { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal OutstandingAmountLcy { get; private set; }
    public decimal AmtRcdNotInvoicedLcy { get; private set; }
    public string BlanketOrderNo { get; private set; }
    public int BlanketOrderLineNo { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal InvDiscAmountToInvoice { get; private set; }
    public string VatIdentifier { get; private set; }
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; }
    public decimal Prepayment { get; private set; }
    public decimal PrepmtLineAmount { get; private set; }
    public decimal PrepmtAmtInv { get; private set; }
    public decimal PrepmtAmtInclVat { get; private set; }
    public decimal PrepaymentAmount { get; private set; }
    public decimal PrepmtVatBaseAmt { get; private set; }
    public decimal PrepaymentVat { get; private set; }
    public short PrepmtVatCalcType { get; private set; }
    public string PrepaymentVatIdentifier { get; private set; }
    public string PrepaymentTaxAreaCode { get; private set; }
    public bool PrepaymentTaxLiable { get; private set; }
    public string PrepaymentTaxGroupCode { get; private set; }
    public decimal PrepmtAmtToDeduct { get; private set; }
    public decimal PrepmtAmtDeducted { get; private set; }
    public bool PrepaymentLine { get; private set; }
    public decimal PrepmtAmountInvInclVat { get; private set; }
    public string IcPartnerCode { get; private set; }
    public int DimensionSetId { get; private set; }
    public string DeferralCode { get; private set; }
    public DateTime? ReturnsDeferralStartDate { get; private set; }
    public int VersionNo { get; private set; }
    public int DocNoOccurrence { get; private set; }
    public string ProdOrderNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal OutstandingQtyBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QtyToReceiveBase { get; private set; }
    public decimal QtyRcdNotInvoicedBase { get; private set; }
    public decimal QtyReceivedBase { get; private set; }
    public decimal QtyInvoicedBase { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public short FaPostingType { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public decimal SalvageValue { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public bool DeprAcquisitionCost { get; private set; }
    public string MaintenanceCode { get; private set; }
    public string InsuranceNo { get; private set; }
    public string BudgetedFaNo { get; private set; }
    public string DuplicateInDepreciationBook { get; private set; }
    public bool UseDuplicationList { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string UnitOfMeasureCrossRef { get; private set; }
    public short CrossReferenceType { get; private set; }
    public string CrossReferenceTypeNo { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public bool SpecialOrder { get; private set; }
    public string SpecialOrderSalesNo { get; private set; }
    public int SpecialOrderSalesLineNo { get; private set; }
    public bool CompletelyReceived { get; private set; }
    public DateTime? RequestedReceiptDate { get; private set; }
    public DateTime? PromisedReceiptDate { get; private set; }
    public string LeadTimeCalculation { get; private set; }
    public string InboundWhseHandlingTime { get; private set; }
    public DateTime? PlannedReceiptDate { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public bool AllowItemChargeAssignment { get; private set; }
    public decimal ReturnQtyToShip { get; private set; }
    public decimal ReturnQtyToShipBase { get; private set; }
    public decimal ReturnQtyShippedNotInvd { get; private set; }
    public decimal RetQtyShpdNotInvdBase { get; private set; }
    public decimal ReturnShpdNotInvd { get; private set; }
    public decimal ReturnShpdNotInvdLcy { get; private set; }
    public decimal ReturnQtyShipped { get; private set; }
    public decimal ReturnQtyShippedBase { get; private set; }
    public string ReturnShipmentNo { get; private set; }
    public int ReturnShipmentLineNo { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public string RoutingNo { get; private set; }
    public string OperationNo { get; private set; }
    public string WorkCenterNo { get; private set; }
    public bool Finished { get; private set; }
    public int ProdOrderLineNo { get; private set; }
    public decimal OverheadRate { get; private set; }
    public bool MpsOrder { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public string SafetyLeadTime { get; private set; }
    public int RoutingReferenceNo { get; private set; }

    public static OperationResult<PurchaseLineArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchaseLineArchive, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchaseLineArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchaseLineArchive, DomainError>.Ok(entity);
    }
}
