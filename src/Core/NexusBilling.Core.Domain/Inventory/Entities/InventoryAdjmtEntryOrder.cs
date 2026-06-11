using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class InventoryAdjmtEntryOrder : Entity
{
    private InventoryAdjmtEntryOrder() { }

    public TenantIdentifier TenantId { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public string ItemNo { get; private set; }
    public string RoutingNo { get; private set; }
    public int RoutingReferenceNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OverheadRate { get; private set; }
    public bool CostIsAdjusted { get; private set; }
    public bool AllowOnlineAdjustment { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal DirectCost { get; private set; }
    public decimal IndirectCostNav { get; private set; }
    public decimal SingleLevelMaterialCost { get; private set; }
    public decimal SingleLevelCapacityCost { get; private set; }
    public decimal SingleLevelSubcontrdCost { get; private set; }
    public decimal SingleLevelCapOvhdCost { get; private set; }
    public decimal SingleLevelMfgOvhdCost { get; private set; }
    public decimal DirectCostAcy { get; private set; }
    public decimal IndirectCostAcy { get; private set; }
    public decimal SingleLvlMaterialCostAcy { get; private set; }
    public decimal SingleLvlCapacityCostAcy { get; private set; }
    public decimal SingleLvlSubcontrdCostAcy { get; private set; }
    public decimal SingleLvlCapOvhdCostAcy { get; private set; }
    public decimal SingleLvlMfgOvhdCostAcy { get; private set; }
    public bool CompletelyInvoiced { get; private set; }
    public bool IsFinished { get; private set; }

    public static OperationResult<InventoryAdjmtEntryOrder, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InventoryAdjmtEntryOrder, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InventoryAdjmtEntryOrder()
        {
            TenantId = tenantId
        };
        return OperationResult<InventoryAdjmtEntryOrder, DomainError>.Ok(entity);
    }
}
