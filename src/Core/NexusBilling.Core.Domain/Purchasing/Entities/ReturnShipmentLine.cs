using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class ReturnShipmentLine : Entity
{
    private ReturnShipmentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string BuyFromVendorNo { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal UnitPriceLcy { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public decimal UnitVolume { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public int ItemShptEntryNo { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string JobNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string PayToVendorNo { get; private set; }
    public string VendorItemNo { get; private set; }
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
    public string BlanketOrderNo { get; private set; }
    public int BlanketOrderLineNo { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobTaskNo { get; private set; }
    public string ProdOrderNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
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
    public decimal ReturnQtyShippedNotInvd { get; private set; }
    public decimal ItemChargeBaseAmount { get; private set; }
    public bool Correction { get; private set; }
    public string ReturnOrderNo { get; private set; }
    public int ReturnOrderLineNo { get; private set; }
    public string ReturnReasonCode { get; private set; }

    public static OperationResult<ReturnShipmentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReturnShipmentLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReturnShipmentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ReturnShipmentLine, DomainError>.Ok(entity);
    }
}
