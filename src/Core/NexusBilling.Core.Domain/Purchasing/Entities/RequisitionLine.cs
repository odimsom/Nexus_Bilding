using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class RequisitionLine : Entity
{
    private RequisitionLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorksheetTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public decimal Quantity { get; private set; }
    public string VendorNo { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public DateTime? DueDate { get; private set; }
    public string RequesterId { get; private set; }
    public bool Confirmed { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public short RecurringMethod { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string RecurringFrequency { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public string VendorItemNo { get; private set; }
    public string SalesOrderNo { get; private set; }
    public int SalesOrderLineNo { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string OrderAddressCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ProdOrderNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public int DemandType { get; private set; }
    public short DemandSubtype { get; private set; }
    public string DemandOrderNo { get; private set; }
    public int DemandLineNo { get; private set; }
    public int DemandRefNo { get; private set; }
    public short Status { get; private set; }
    public DateTime? DemandDate { get; private set; }
    public decimal DemandQuantity { get; private set; }
    public decimal DemandQuantityBase { get; private set; }
    public decimal NeededQuantity { get; private set; }
    public decimal NeededQuantityBase { get; private set; }
    public bool Reserve { get; private set; }
    public decimal QtyPerUomDemand { get; private set; }
    public string UnitOfMeasureCodeDemand { get; private set; }
    public string SupplyFrom { get; private set; }
    public string OriginalItemNo { get; private set; }
    public string OriginalVariantCode { get; private set; }
    public int Level { get; private set; }
    public decimal DemandQtyAvailable { get; private set; }
    public string UserId { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public string TransferFromCode { get; private set; }
    public DateTime? TransferShipmentDate { get; private set; }
    public decimal LineDiscount { get; private set; }
    public string RoutingNo { get; private set; }
    public string OperationNo { get; private set; }
    public string WorkCenterNo { get; private set; }
    public int ProdOrderLineNo { get; private set; }
    public bool MpsOrder { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public int RoutingReferenceNo { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string GenBusinessPostingGroup { get; private set; }
    public int LowLevelCode { get; private set; }
    public string ProductionBomVersionCode { get; private set; }
    public string RoutingVersionCode { get; private set; }
    public short RoutingType { get; private set; }
    public decimal OriginalQuantity { get; private set; }
    public decimal FinishedQuantity { get; private set; }
    public decimal RemainingQuantity { get; private set; }
    public DateTime? OriginalDueDate { get; private set; }
    public decimal Scrap { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string StartingTime { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string EndingTime { get; private set; }
    public string ProductionBomNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OverheadRate { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public short ReplenishmentSystem { get; private set; }
    public string RefOrderNo { get; private set; }
    public short RefOrderType { get; private set; }
    public short RefOrderStatus { get; private set; }
    public int RefLineNo { get; private set; }
    public string NoSeries { get; private set; }
    public decimal FinishedQtyBase { get; private set; }
    public decimal RemainingQtyBase { get; private set; }
    public int RelatedToPlanningLine { get; private set; }
    public int PlanningLevel { get; private set; }
    public short PlanningLineOrigin { get; private set; }
    public short ActionMessage { get; private set; }
    public bool AcceptActionMessage { get; private set; }
    public decimal NetQuantityBase { get; private set; }
    public DateTime? StartingDateTime { get; private set; }
    public DateTime? EndingDateTime { get; private set; }
    public string OrderPromisingId { get; private set; }
    public int OrderPromisingLineNo { get; private set; }
    public int OrderPromisingLineId { get; private set; }

    public static OperationResult<RequisitionLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RequisitionLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RequisitionLine()
        {
            TenantId = tenantId
        };
        return OperationResult<RequisitionLine, DomainError>.Ok(entity);
    }
}
