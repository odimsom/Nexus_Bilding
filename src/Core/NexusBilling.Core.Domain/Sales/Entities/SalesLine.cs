using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesLine : Entity
{
    private SalesLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string? SellToCustomerNo { get; set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; set; }
    public string No { get; set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string Description { get; set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; set; }
    public decimal Quantity { get; set; }
    public decimal OutstandingQuantity { get; set; }
    public decimal QtyToInvoice { get; set; }
    public decimal QtyToShip { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; set; }
    public decimal LineDiscount { get; set; }
    public decimal LineDiscountAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public bool AllowInvoiceDisc { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public decimal UnitVolume { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public string? JobNo { get; private set; }
    public string WorkTypeCode { get; private set; }
    public bool RecalculateInvoiceDisc { get; private set; }
    public decimal OutstandingAmount { get; set; }
    public decimal QtyShippedNotInvoiced { get; private set; }
    public decimal ShippedNotInvoiced { get; private set; }
    public decimal QuantityShipped { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ShipmentNo { get; private set; }
    public int ShipmentLineNo { get; private set; }
    public decimal Profit { get; private set; }
    public string? BillToCustomerNo { get; private set; }
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
    public string? Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string TaxCategory { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatClauseCode { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string? CurrencyCode { get; private set; }
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
    public decimal PrepmtAmountInvLcy { get; private set; }
    public string IcPartnerCode { get; private set; }
    public decimal PrepmtVatAmountInvLcy { get; private set; }

    public static OperationResult<SalesLine, DomainError> Create(
        TenantIdentifier tenantId,
        short documentType,
        string documentNo,
        int lineNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesLine, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(documentNo))
            return OperationResult<SalesLine, DomainError>.Fail(DomainError.Validation("sales.document_no_required", "El número de documento es obligatorio."));

        var entity = new SalesLine
        {
            TenantId = tenantId,
            DocumentType = documentType,
            DocumentNo = documentNo.Trim(),
            LineNo = lineNo,
            No = string.Empty,
            LocationCode = string.Empty,
            PostingGroup = string.Empty,
            Description = string.Empty,
            Description2 = string.Empty,
            UnitOfMeasure = string.Empty,
            ShortcutDimension1Code = string.Empty,
            ShortcutDimension2Code = string.Empty,
            CustomerPriceGroup = string.Empty,
            WorkTypeCode = string.Empty,
            ShipmentNo = string.Empty,
            PurchaseOrderNo = string.Empty,
            GenBusPostingGroup = string.Empty,
            GenProdPostingGroup = string.Empty,
            TransactionType = string.Empty,
            TransportMethod = string.Empty,
            ExitPoint = string.Empty,
            TransactionSpecification = string.Empty,
            TaxCategory = string.Empty,
            TaxAreaCode = string.Empty,
            TaxGroupCode = string.Empty,
            VatClauseCode = string.Empty,
            VatBusPostingGroup = string.Empty,
            VatProdPostingGroup = string.Empty,
            BlanketOrderNo = string.Empty,
            VatIdentifier = string.Empty,
            IcPartnerReference = string.Empty,
            PrepaymentVatIdentifier = string.Empty,
            PrepaymentTaxAreaCode = string.Empty,
            PrepaymentTaxGroupCode = string.Empty,
            IcPartnerCode = string.Empty
        };

        return OperationResult<SalesLine, DomainError>.Ok(entity);
    }
}
