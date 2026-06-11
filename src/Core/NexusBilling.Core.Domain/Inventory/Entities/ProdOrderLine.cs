using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProdOrderLine : Entity
{
    private ProdOrderLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Status { get; private set; }
    public string ProdOrderNo { get; private set; }
    public int LineNo { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string LocationCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string BinCode { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal FinishedQuantity { get; private set; }
    public decimal RemainingQuantity { get; private set; }
    public decimal Scrap { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string StartingTime { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string EndingTime { get; private set; }
    public int PlanningLevelCode { get; private set; }
    public int Priority { get; private set; }
    public string ProductionBomNo { get; private set; }
    public string RoutingNo { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public int RoutingReferenceNo { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal FinishedQtyBase { get; private set; }
    public decimal RemainingQtyBase { get; private set; }
    public DateTime? StartingDateTime { get; private set; }
    public DateTime? EndingDateTime { get; private set; }
    public int DimensionSetId { get; private set; }
    public decimal CostAmountAcy { get; private set; }
    public decimal UnitCostAcy { get; private set; }
    public string ProductionBomVersionCode { get; private set; }
    public string RoutingVersionCode { get; private set; }
    public short RoutingType { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public bool MpsOrder { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OverheadRate { get; private set; }

    public static OperationResult<ProdOrderLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProdOrderLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProdOrderLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ProdOrderLine, DomainError>.Ok(entity);
    }
}
