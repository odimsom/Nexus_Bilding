using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesInvoiceLine : Entity
{
    private SalesInvoiceLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
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
    public string CustomerPriceGroup { get; private set; }
    public string JobNo { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string ShipmentNo { get; private set; }
    public int ShipmentLineNo { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public decimal InvDiscountAmount { get; private set; }
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
    public string TaxCategory { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public string VatClauseCode { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string BlanketOrderNo { get; private set; }
    public int BlanketOrderLineNo { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public string VatIdentifier { get; private set; }
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; }
    public bool PrepaymentLine { get; private set; }
    public string IcPartnerCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobTaskNo { get; private set; }
    public int JobContractEntryNo { get; private set; }
    public string DeferralCode { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
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
    public int ApplFromItemEntry { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public string CustomerDiscGroup { get; private set; }

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
