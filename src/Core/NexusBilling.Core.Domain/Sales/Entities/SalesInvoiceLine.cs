using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesInvoiceLine : Entity
{
    private SalesInvoiceLine() { }

    public TenantIdentifier TenantId { get; set; }
    public string SellToCustomerNo { get; set; }
    public string DocumentNo { get; set; }
    public int LineNo { get; set; }
    public short Type { get; set; }
    public string No { get; set; }
    public string LocationCode { get; set; }
    public string PostingGroup { get; set; }
    public DateTime? ShipmentDate { get; set; }
    public string Description { get; set; }
    public string Description2 { get; set; }
    public string UnitOfMeasure { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitCostLcy { get; set; }
    public decimal Vat { get; set; }
    public decimal LineDiscount { get; set; }
    public decimal LineDiscountAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public bool AllowInvoiceDisc { get; set; }
    public decimal GrossWeight { get; set; }
    public decimal NetWeight { get; set; }
    public decimal UnitsPerParcel { get; set; }
    public decimal UnitVolume { get; set; }
    public int ApplToItemEntry { get; set; }
    public string ShortcutDimension1Code { get; set; }
    public string ShortcutDimension2Code { get; set; }
    public string CustomerPriceGroup { get; set; }
    public string JobNo { get; set; }
    public string WorkTypeCode { get; set; }
    public string ShipmentNo { get; set; }
    public int ShipmentLineNo { get; set; }
    public string BillToCustomerNo { get; set; }
    public decimal InvDiscountAmount { get; set; }
    public bool DropShipment { get; set; }
    public string GenBusPostingGroup { get; set; }
    public string GenProdPostingGroup { get; set; }
    public short VatCalculationType { get; set; }
    public string TransactionType { get; set; }
    public string TransportMethod { get; set; }
    public int AttachedToLineNo { get; set; }
    public string ExitPoint { get; set; }
    public string Area { get; set; }
    public string TransactionSpecification { get; set; }
    public string TaxCategory { get; set; }
    public string TaxAreaCode { get; set; }
    public bool TaxLiable { get; set; }
    public string TaxGroupCode { get; set; }
    public string VatClauseCode { get; set; }
    public string VatBusPostingGroup { get; set; }
    public string VatProdPostingGroup { get; set; }
    public string BlanketOrderNo { get; set; }
    public int BlanketOrderLineNo { get; set; }
    public decimal VatBaseAmount { get; set; }
    public decimal UnitCost { get; set; }
    public bool SystemCreatedEntry { get; set; }
    public decimal LineAmount { get; set; }
    public decimal VatDifference { get; set; }
    public string VatIdentifier { get; set; }
    public short IcPartnerRefType { get; set; }
    public string IcPartnerReference { get; set; }
    public bool PrepaymentLine { get; set; }
    public string IcPartnerCode { get; set; }
    public DateTime? PostingDate { get; set; }
    public int DimensionSetId { get; set; }
    public string JobTaskNo { get; set; }
    public int JobContractEntryNo { get; set; }
    public string DeferralCode { get; set; }
    public string VariantCode { get; set; }
    public string BinCode { get; set; }
    public decimal QtyPerUnitOfMeasure { get; set; }
    public string UnitOfMeasureCode { get; set; }
    public decimal QuantityBase { get; set; }
    public DateTime? FaPostingDate { get; set; }
    public string DepreciationBookCode { get; set; }
    public bool DeprUntilFaPostingDate { get; set; }
    public string DuplicateInDepreciationBook { get; set; }
    public bool UseDuplicationList { get; set; }
    public string ResponsibilityCenter { get; set; }
    public string CrossReferenceNo { get; set; }
    public string UnitOfMeasureCrossRef { get; set; }
    public short CrossReferenceType { get; set; }
    public string CrossReferenceTypeNo { get; set; }
    public string ItemCategoryCode { get; set; }
    public bool Nonstock { get; set; }
    public string PurchasingCode { get; set; }
    public string ProductGroupCode { get; set; }
    public int ApplFromItemEntry { get; set; }
    public string ReturnReasonCode { get; set; }
    public bool AllowLineDisc { get; set; }
    public string CustomerDiscGroup { get; set; }

    public static OperationResult<SalesInvoiceLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesInvoiceLine, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesInvoiceLine()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesInvoiceLine, DomainError>.Ok(entity);
    }
}
