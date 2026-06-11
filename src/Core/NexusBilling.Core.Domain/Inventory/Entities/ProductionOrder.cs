using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProductionOrder : Entity
{
    private ProductionOrder() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Status { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string SearchDescription { get; private set; }
    public string Description2 { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public string RoutingNo { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string StartingTime { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string EndingTime { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? FinishedDate { get; private set; }
    public bool Blocked { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public string BinCode { get; private set; }
    public string ReplanRefNo { get; private set; }
    public short ReplanRefStatus { get; private set; }
    public int LowLevelCode { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public string NoSeries { get; private set; }
    public string PlannedOrderNo { get; private set; }
    public string FirmPlannedOrderNo { get; private set; }
    public string SimulatedOrderNo { get; private set; }
    public DateTime? StartingDateTime { get; private set; }
    public DateTime? EndingDateTime { get; private set; }
    public int DimensionSetId { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<ProductionOrder, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProductionOrder, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProductionOrder()
        {
            TenantId = tenantId
        };
        return OperationResult<ProductionOrder, DomainError>.Ok(entity);
    }
}
