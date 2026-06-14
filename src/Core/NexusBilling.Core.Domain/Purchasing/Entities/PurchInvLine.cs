using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchInvLine : Entity
{
    private PurchInvLine()
    {
        BuyFromVendorNo = string.Empty; DocumentNo = string.Empty; No = string.Empty;
        LocationCode = string.Empty; PostingGroup = string.Empty; Description = string.Empty;
        Description2 = string.Empty; UnitOfMeasure = string.Empty; ShortcutDimension1Code = string.Empty;
        ShortcutDimension2Code = string.Empty; JobNo = string.Empty; ReceiptNo = string.Empty;
        PayToVendorNo = string.Empty; VendorItemNo = string.Empty; GenBusPostingGroup = string.Empty;
        GenProdPostingGroup = string.Empty; TransactionType = string.Empty; TransportMethod = string.Empty;
        EntryPoint = string.Empty; Area = string.Empty; TransactionSpecification = string.Empty;
        TaxAreaCode = string.Empty; TaxGroupCode = string.Empty; VatBusPostingGroup = string.Empty;
        VatProdPostingGroup = string.Empty; BlanketOrderNo = string.Empty; VatIdentifier = string.Empty;
        IcPartnerReference = string.Empty; IcPartnerCode = string.Empty; JobTaskNo = string.Empty;
        JobCurrencyCode = string.Empty; DeferralCode = string.Empty; ProdOrderNo = string.Empty;
        VariantCode = string.Empty; BinCode = string.Empty; UnitOfMeasureCode = string.Empty;
        DepreciationBookCode = string.Empty; MaintenanceCode = string.Empty; InsuranceNo = string.Empty;
        BudgetedFaNo = string.Empty; DuplicateInDepreciationBook = string.Empty;
        ResponsibilityCenter = string.Empty; CrossReferenceNo = string.Empty;
        UnitOfMeasureCrossRef = string.Empty; CrossReferenceTypeNo = string.Empty;
        ItemCategoryCode = string.Empty; PurchasingCode = string.Empty; ProductGroupCode = string.Empty;
        ReturnReasonCode = string.Empty; RoutingNo = string.Empty; OperationNo = string.Empty;
        WorkCenterNo = string.Empty;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string BuyFromVendorNo { get; private set; } = string.Empty;
    public string DocumentNo { get; private set; } = string.Empty;
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; } = string.Empty;
    public string LocationCode { get; private set; } = string.Empty;
    public string PostingGroup { get; private set; } = string.Empty;
    public DateTime? ExpectedReceiptDate { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Description2 { get; private set; } = string.Empty;
    public string UnitOfMeasure { get; private set; } = string.Empty;
    public decimal Quantity { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
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
    public string ShortcutDimension1Code { get; private set; } = string.Empty;
    public string ShortcutDimension2Code { get; private set; } = string.Empty;
    public string JobNo { get; private set; } = string.Empty;
    public decimal IndirectCost { get; private set; }
    public string ReceiptNo { get; private set; } = string.Empty;
    public int ReceiptLineNo { get; private set; }
    public string PayToVendorNo { get; private set; } = string.Empty;
    public decimal InvDiscountAmount { get; private set; }
    public string VendorItemNo { get; private set; } = string.Empty;
    public string GenBusPostingGroup { get; private set; } = string.Empty;
    public string GenProdPostingGroup { get; private set; } = string.Empty;
    public short VatCalculationType { get; private set; }
    public string TransactionType { get; private set; } = string.Empty;
    public string TransportMethod { get; private set; } = string.Empty;
    public int AttachedToLineNo { get; private set; }
    public string EntryPoint { get; private set; } = string.Empty;
    public string Area { get; private set; } = string.Empty;
    public string TransactionSpecification { get; private set; } = string.Empty;
    public string TaxAreaCode { get; private set; } = string.Empty;
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; } = string.Empty;
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; } = string.Empty;
    public string VatProdPostingGroup { get; private set; } = string.Empty;
    public string BlanketOrderNo { get; private set; } = string.Empty;
    public int BlanketOrderLineNo { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public string VatIdentifier { get; private set; } = string.Empty;
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; } = string.Empty;
    public bool PrepaymentLine { get; private set; }
    public string IcPartnerCode { get; private set; } = string.Empty;
    public DateTime? PostingDate { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobTaskNo { get; private set; } = string.Empty;
    public short JobLineType { get; private set; }
    public decimal JobUnitPrice { get; private set; }
    public decimal JobTotalPrice { get; private set; }
    public decimal JobLineAmount { get; private set; }
    public decimal JobLineDiscountAmount { get; private set; }
    public decimal JobLineDiscount { get; private set; }
    public decimal JobUnitPriceLcy { get; private set; }
    public decimal JobTotalPriceLcy { get; private set; }
    public decimal JobLineAmountLcy { get; private set; }
    public decimal JobLineDiscAmountLcy { get; private set; }
    public decimal JobCurrencyFactor { get; private set; }
    public string JobCurrencyCode { get; private set; } = string.Empty;
    public string DeferralCode { get; private set; } = string.Empty;
    public string ProdOrderNo { get; private set; } = string.Empty;
    public string VariantCode { get; private set; } = string.Empty;
    public string BinCode { get; private set; } = string.Empty;
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; } = string.Empty;
    public decimal QuantityBase { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public short FaPostingType { get; private set; }
    public string DepreciationBookCode { get; private set; } = string.Empty;
    public decimal SalvageValue { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public bool DeprAcquisitionCost { get; private set; }
    public string MaintenanceCode { get; private set; } = string.Empty;
    public string InsuranceNo { get; private set; } = string.Empty;
    public string BudgetedFaNo { get; private set; } = string.Empty;
    public string DuplicateInDepreciationBook { get; private set; } = string.Empty;
    public bool UseDuplicationList { get; private set; }
    public string ResponsibilityCenter { get; private set; } = string.Empty;
    public string CrossReferenceNo { get; private set; } = string.Empty;
    public string UnitOfMeasureCrossRef { get; private set; } = string.Empty;
    public short CrossReferenceType { get; private set; }
    public string CrossReferenceTypeNo { get; private set; } = string.Empty;
    public string ItemCategoryCode { get; private set; } = string.Empty;
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; } = string.Empty;
    public string ProductGroupCode { get; private set; } = string.Empty;
    public string ReturnReasonCode { get; private set; } = string.Empty;
    public string RoutingNo { get; private set; } = string.Empty;
    public string OperationNo { get; private set; } = string.Empty;
    public string WorkCenterNo { get; private set; } = string.Empty;
    public int ProdOrderLineNo { get; private set; }
    public decimal OverheadRate { get; private set; }
    public int RoutingReferenceNo { get; private set; }

    public static OperationResult<PurchInvLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchInvLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchInvLine()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchInvLine, DomainError>.Ok(entity);
    }
}
