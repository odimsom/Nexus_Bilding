using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesInvoiceLine : Entity
{
    private SalesInvoiceLine() { }

    public TenantIdentifier TenantId { get; set; }
    public string SellToCustomerNo { get; set; } = string.Empty;
    public string DocumentNo { get; set; } = string.Empty;
    public int LineNo { get; set; }
    public short Type { get; set; }
    public string No { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public string PostingGroup { get; set; } = string.Empty;
    public DateTime? ShipmentDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Description2 { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
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
    public string ShortcutDimension1Code { get; set; } = string.Empty;
    public string ShortcutDimension2Code { get; set; } = string.Empty;
    public string CustomerPriceGroup { get; set; } = string.Empty;
    public string JobNo { get; set; } = string.Empty;
    public string WorkTypeCode { get; set; } = string.Empty;
    public string ShipmentNo { get; set; } = string.Empty;
    public int ShipmentLineNo { get; set; }
    public string BillToCustomerNo { get; set; } = string.Empty;
    public decimal InvDiscountAmount { get; set; }
    public bool DropShipment { get; set; }
    public string GenBusPostingGroup { get; set; } = string.Empty;
    public string GenProdPostingGroup { get; set; } = string.Empty;
    public short VatCalculationType { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string TransportMethod { get; set; } = string.Empty;
    public int AttachedToLineNo { get; set; }
    public string ExitPoint { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string TransactionSpecification { get; set; } = string.Empty;
    public string TaxCategory { get; set; } = string.Empty;
    public string TaxAreaCode { get; set; } = string.Empty;
    public bool TaxLiable { get; set; }
    public string TaxGroupCode { get; set; } = string.Empty;
    public string VatClauseCode { get; set; } = string.Empty;
    public string VatBusPostingGroup { get; set; } = string.Empty;
    public string VatProdPostingGroup { get; set; } = string.Empty;
    public string BlanketOrderNo { get; set; } = string.Empty;
    public int BlanketOrderLineNo { get; set; }
    public decimal VatBaseAmount { get; set; }
    public decimal UnitCost { get; set; }
    public bool SystemCreatedEntry { get; set; }
    public decimal LineAmount { get; set; }
    public decimal VatDifference { get; set; }
    public string VatIdentifier { get; set; } = string.Empty;
    public short IcPartnerRefType { get; set; }
    public string IcPartnerReference { get; set; } = string.Empty;
    public bool PrepaymentLine { get; set; }
    public string IcPartnerCode { get; set; } = string.Empty;
    public DateTime? PostingDate { get; set; }
    public int DimensionSetId { get; set; }
    public string JobTaskNo { get; set; } = string.Empty;
    public int JobContractEntryNo { get; set; }
    public string DeferralCode { get; set; } = string.Empty;
    public string VariantCode { get; set; } = string.Empty;
    public string BinCode { get; set; } = string.Empty;
    public decimal QtyPerUnitOfMeasure { get; set; }
    public string UnitOfMeasureCode { get; set; } = string.Empty;
    public decimal QuantityBase { get; set; }
    public DateTime? FaPostingDate { get; set; }
    public string DepreciationBookCode { get; set; } = string.Empty;
    public bool DeprUntilFaPostingDate { get; set; }
    public string DuplicateInDepreciationBook { get; set; } = string.Empty;
    public bool UseDuplicationList { get; set; }
    public string ResponsibilityCenter { get; set; } = string.Empty;
    public string CrossReferenceNo { get; set; } = string.Empty;
    public string UnitOfMeasureCrossRef { get; set; } = string.Empty;
    public short CrossReferenceType { get; set; }
    public string CrossReferenceTypeNo { get; set; } = string.Empty;
    public string ItemCategoryCode { get; set; } = string.Empty;
    public bool Nonstock { get; set; }
    public string PurchasingCode { get; set; } = string.Empty;
    public string ProductGroupCode { get; set; } = string.Empty;
    public int ApplFromItemEntry { get; set; }
    public string ReturnReasonCode { get; set; } = string.Empty;
    public bool AllowLineDisc { get; set; }
    public string CustomerDiscGroup { get; set; } = string.Empty;

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
