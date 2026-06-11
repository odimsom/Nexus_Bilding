using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesLineArchive : Entity
{
    private SalesLineArchive() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public string QuantityDiscCode { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal OutstandingQuantity { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public decimal QtyToShip { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
    public decimal QuantityDisc { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountIncludingVat { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public decimal UnitVolume { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string PriceGroupCode { get; private set; }
    public bool AllowQuantityDisc { get; private set; }
    public string JobNo { get; private set; }
    public string WorkTypeCode { get; private set; }
    public decimal CustItemDisc { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public decimal QtyShippedNotInvoiced { get; private set; }
    public decimal ShippedNotInvoiced { get; private set; }
    public decimal QuantityShipped { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ShipmentNo { get; private set; }
    public int ShipmentLineNo { get; private set; }
    public decimal Profit { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public decimal InvDiscountAmount { get; private set; }
    public string PurchaseOrderNo { get; private set; }
    public int PurchOrderLineNo { get; private set; }
    public bool DropShipment { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public short VatCalculationType { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public int AttachedToLineNo { get; private set; }
    public string ExitPoint { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal OutstandingAmountLcy { get; private set; }
    public decimal ShippedNotInvoicedLcy { get; private set; }
    public short Reserve { get; private set; }
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
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public bool Planned { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal OutstandingQtyBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QtyToShipBase { get; private set; }
    public decimal QtyShippedNotInvdBase { get; private set; }
    public decimal QtyShippedBase { get; private set; }
    public decimal QtyInvoicedBase { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public string DuplicateInDepreciationBook { get; private set; }
    public bool UseDuplicationList { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public bool OutOfStockSubstitution { get; private set; }
    public string OriginallyOrderedNo { get; private set; }
    public string OriginallyOrderedVarCode { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string UnitOfMeasureCrossRef { get; private set; }
    public short CrossReferenceType { get; private set; }
    public string CrossReferenceTypeNo { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public bool SpecialOrder { get; private set; }
    public string SpecialOrderPurchaseNo { get; private set; }
    public int SpecialOrderPurchLineNo { get; private set; }
    public bool CompletelyShipped { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public string ShippingTime { get; private set; }
    public string OutboundWhseHandlingTime { get; private set; }
    public DateTime? PlannedDeliveryDate { get; private set; }
    public DateTime? PlannedShipmentDate { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public bool AllowItemChargeAssignment { get; private set; }
    public decimal ReturnQtyToReceive { get; private set; }
    public decimal ReturnQtyToReceiveBase { get; private set; }
    public decimal ReturnQtyRcdNotInvd { get; private set; }
    public decimal RetQtyRcdNotInvdBase { get; private set; }
    public decimal ReturnAmtRcdNotInvd { get; private set; }
    public decimal RetAmtRcdNotInvdLcy { get; private set; }
    public decimal ReturnQtyReceived { get; private set; }
    public decimal ReturnQtyReceivedBase { get; private set; }
    public int ApplFromItemEntry { get; private set; }
    public string ServiceContractNo { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public string ServiceItemNo { get; private set; }
    public int ApplToServiceEntry { get; private set; }
    public int ServiceItemLineNo { get; private set; }
    public string ServPriceAdjmtGrCode { get; private set; }
    public string BomItemNo { get; private set; }
    public string ReturnReceiptNo { get; private set; }
    public int ReturnReceiptLineNo { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public string CustomerDiscGroup { get; private set; }

    public static OperationResult<SalesLineArchive, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesLineArchive, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesLineArchive()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesLineArchive, DomainError>.Ok(entity);
    }
}
