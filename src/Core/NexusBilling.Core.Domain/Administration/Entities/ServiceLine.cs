using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceLine : Entity
{
    private ServiceLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string CustomerNo { get; private set; }
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
    public decimal OutstandingQuantity { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public decimal QtyToShip { get; private set; }
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
    public string JobTaskNo { get; private set; }
    public short JobLineType { get; private set; }
    public string WorkTypeCode { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public decimal QtyShippedNotInvoiced { get; private set; }
    public decimal ShippedNotInvoiced { get; private set; }
    public decimal QuantityShipped { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ShipmentNo { get; private set; }
    public int ShipmentLineNo { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public decimal InvDiscountAmount { get; private set; }
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
    public string VatClauseCode { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal OutstandingAmountLcy { get; private set; }
    public decimal ShippedNotInvoicedLcy { get; private set; }
    public short Reserve { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal InvDiscAmountToInvoice { get; private set; }
    public string VatIdentifier { get; private set; }
    public int DimensionSetId { get; private set; }
    public string TimeSheetNo { get; private set; }
    public int TimeSheetLineNo { get; private set; }
    public DateTime? TimeSheetDate { get; private set; }
    public int JobPlanningLineNo { get; private set; }
    public decimal JobRemainingQty { get; private set; }
    public decimal JobRemainingQtyBase { get; private set; }
    public decimal JobRemainingTotalCost { get; private set; }
    public decimal JobRemainingTotalCostLcy { get; private set; }
    public decimal JobRemainingLineAmount { get; private set; }
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
    public string ResponsibilityCenter { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string ProductGroupCode { get; private set; }
    public bool CompletelyShipped { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public string ShippingTime { get; private set; }
    public DateTime? PlannedDeliveryDate { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public int ApplFromItemEntry { get; private set; }
    public string ServiceItemNo { get; private set; }
    public int ApplToServiceEntry { get; private set; }
    public int ServiceItemLineNo { get; private set; }
    public string ServiceItemSerialNo { get; private set; }
    public string ServPriceAdjmtGrCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public DateTime? NeededByDate { get; private set; }
    public string ShipToCode { get; private set; }
    public decimal QtyToConsume { get; private set; }
    public decimal QuantityConsumed { get; private set; }
    public decimal QtyToConsumeBase { get; private set; }
    public decimal QtyConsumedBase { get; private set; }
    public string ServicePriceGroupCode { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string SymptomCode { get; private set; }
    public string FaultCode { get; private set; }
    public string ResolutionCode { get; private set; }
    public bool ExcludeWarranty { get; private set; }
    public bool Warranty { get; private set; }
    public string ContractNo { get; private set; }
    public decimal ContractDisc { get; private set; }
    public decimal WarrantyDisc { get; private set; }
    public int ComponentLineNo { get; private set; }
    public short SparePartAction { get; private set; }
    public string FaultReasonCode { get; private set; }
    public string ReplacedItemNo { get; private set; }
    public bool ExcludeContractDiscount { get; private set; }
    public short ReplacedItemType { get; private set; }
    public short PriceAdjmtStatus { get; private set; }
    public short LineDiscountType { get; private set; }
    public short CopyComponentsFrom { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public bool AllowLineDisc { get; private set; }
    public string CustomerDiscGroup { get; private set; }
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
