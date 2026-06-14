using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceLine : Entity
{
    private ServiceLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; set; }
    public string CustomerNo { get; set; } = string.Empty;
    public string DocumentNo { get; set; } = string.Empty;
    public int LineNo { get; set; }
    public short Type { get; set; }
    public string No { get; set; } = string.Empty;
    public string LocationCode { get; private set; } = string.Empty;
    public string PostingGroup { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Description2 { get; private set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
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
    public string ShortcutDimension1Code { get; private set; } = string.Empty;
    public string ShortcutDimension2Code { get; private set; } = string.Empty;
    public string CustomerPriceGroup { get; private set; } = string.Empty;
    public string JobNo { get; private set; } = string.Empty;
    public string JobTaskNo { get; private set; } = string.Empty;
    public short JobLineType { get; private set; }
    public string WorkTypeCode { get; private set; } = string.Empty;
    public decimal OutstandingAmount { get; set; }
    public decimal QtyShippedNotInvoiced { get; private set; }
    public decimal ShippedNotInvoiced { get; private set; }
    public decimal QuantityShipped { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ShipmentNo { get; private set; } = string.Empty;
    public int ShipmentLineNo { get; private set; }
    public string BillToCustomerNo { get; private set; } = string.Empty;
    public decimal InvDiscountAmount { get; private set; }
    public string GenBusPostingGroup { get; private set; } = string.Empty;
    public string GenProdPostingGroup { get; private set; } = string.Empty;
    public short VatCalculationType { get; private set; }
    public string TransactionType { get; private set; } = string.Empty;
    public string TransportMethod { get; private set; } = string.Empty;
    public int AttachedToLineNo { get; private set; }
    public string ExitPoint { get; private set; } = string.Empty;
    public string Area { get; private set; } = string.Empty;
    public string TransactionSpecification { get; private set; } = string.Empty;
    public string TaxAreaCode { get; private set; } = string.Empty;
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; } = string.Empty;
    public string VatClauseCode { get; private set; } = string.Empty;
    public string VatBusPostingGroup { get; private set; } = string.Empty;
    public string VatProdPostingGroup { get; private set; } = string.Empty;
    public string CurrencyCode { get; private set; } = string.Empty;
    public decimal OutstandingAmountLcy { get; private set; }
    public decimal ShippedNotInvoicedLcy { get; private set; }
    public short Reserve { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal InvDiscAmountToInvoice { get; private set; }
    public string VatIdentifier { get; private set; } = string.Empty;
    public int DimensionSetId { get; private set; }
    public string TimeSheetNo { get; private set; } = string.Empty;
    public int TimeSheetLineNo { get; private set; }
    public DateTime? TimeSheetDate { get; private set; }
    public int JobPlanningLineNo { get; private set; }
    public decimal JobRemainingQty { get; private set; }
    public decimal JobRemainingQtyBase { get; private set; }
    public decimal JobRemainingTotalCost { get; private set; }
    public decimal JobRemainingTotalCostLcy { get; private set; }
    public decimal JobRemainingLineAmount { get; private set; }
    public string VariantCode { get; private set; } = string.Empty;
    public string BinCode { get; private set; } = string.Empty;
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public bool Planned { get; private set; }
    public string UnitOfMeasureCode { get; private set; } = string.Empty;
    public decimal QuantityBase { get; private set; }
    public decimal OutstandingQtyBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QtyToShipBase { get; private set; }
    public decimal QtyShippedNotInvdBase { get; private set; }
    public decimal QtyShippedBase { get; private set; }
    public decimal QtyInvoicedBase { get; private set; }
    public string ResponsibilityCenter { get; private set; } = string.Empty;
    public string ItemCategoryCode { get; private set; } = string.Empty;
    public bool Nonstock { get; private set; }
    public string ProductGroupCode { get; private set; } = string.Empty;
    public bool CompletelyShipped { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public string ShippingTime { get; private set; } = string.Empty;
    public DateTime? PlannedDeliveryDate { get; private set; }
    public string ShippingAgentCode { get; private set; } = string.Empty;
    public string ShippingAgentServiceCode { get; private set; } = string.Empty;
    public int ApplFromItemEntry { get; private set; }
    public string ServiceItemNo { get; private set; } = string.Empty;
    public int ApplToServiceEntry { get; private set; }
    public int ServiceItemLineNo { get; private set; }
    public string ServiceItemSerialNo { get; private set; } = string.Empty;
    public string ServPriceAdjmtGrCode { get; private set; } = string.Empty;
    public DateTime? PostingDate { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public DateTime? NeededByDate { get; private set; }
    public string ShipToCode { get; private set; } = string.Empty;
    public decimal QtyToConsume { get; private set; }
    public decimal QuantityConsumed { get; private set; }
    public decimal QtyToConsumeBase { get; private set; }
    public decimal QtyConsumedBase { get; private set; }
    public string ServicePriceGroupCode { get; private set; } = string.Empty;
    public string FaultAreaCode { get; private set; } = string.Empty;
    public string SymptomCode { get; private set; } = string.Empty;
    public string FaultCode { get; private set; } = string.Empty;
    public string ResolutionCode { get; private set; } = string.Empty;
    public bool ExcludeWarranty { get; private set; }
    public bool Warranty { get; private set; }
    public string ContractNo { get; private set; } = string.Empty;
    public decimal ContractDisc { get; private set; }
    public decimal WarrantyDisc { get; private set; }
    public int ComponentLineNo { get; private set; }
    public short SparePartAction { get; private set; }
    public string FaultReasonCode { get; private set; } = string.Empty;
    public string ReplacedItemNo { get; private set; } = string.Empty;
    public bool ExcludeContractDiscount { get; private set; }
    public short ReplacedItemType { get; private set; }
    public short PriceAdjmtStatus { get; private set; }
    public short LineDiscountType { get; private set; }
    public short CopyComponentsFrom { get; private set; }
    public string ReturnReasonCode { get; private set; } = string.Empty;
    public bool AllowLineDisc { get; private set; }
    public string CustomerDiscGroup { get; private set; } = string.Empty;
    public decimal QtyPicked { get; private set; }
    public decimal QtyPickedBase { get; private set; }
    public bool CompletelyPicked { get; private set; }
    public decimal PickQtyBase { get; private set; }

    public static OperationResult<ServiceLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceLine, DomainError>.Ok(entity);
    }
}
