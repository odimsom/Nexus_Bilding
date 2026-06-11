using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class StockkeepingUnit : Entity
{
    private StockkeepingUnit() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string LocationCode { get; private set; }
    public string ShelfNo { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal StandardCost { get; private set; }
    public decimal LastDirectCost { get; private set; }
    public string VendorNo { get; private set; }
    public string VendorItemNo { get; private set; }
    public string LeadTimeCalculation { get; private set; }
    public decimal ReorderPoint { get; private set; }
    public decimal MaximumInventory { get; private set; }
    public decimal ReorderQuantity { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public short AssemblyPolicy { get; private set; }
    public int TransferLevelCode { get; private set; }
    public decimal LotSize { get; private set; }
    public int DiscreteOrderQuantity { get; private set; }
    public decimal MinimumOrderQuantity { get; private set; }
    public decimal MaximumOrderQuantity { get; private set; }
    public decimal SafetyStockQuantity { get; private set; }
    public decimal OrderMultiple { get; private set; }
    public string SafetyLeadTime { get; private set; }
    public string ComponentsAtLocation { get; private set; }
    public short FlushingMethod { get; private set; }
    public short ReplenishmentSystem { get; private set; }
    public string TimeBucket { get; private set; }
    public short ReorderingPolicy { get; private set; }
    public bool IncludeInventory { get; private set; }
    public short ManufacturingPolicy { get; private set; }
    public string ReschedulingPeriod { get; private set; }
    public string LotAccumulationPeriod { get; private set; }
    public string DampenerPeriod { get; private set; }
    public decimal DampenerQuantity { get; private set; }
    public decimal OverflowLevel { get; private set; }
    public string TransferFromCode { get; private set; }
    public string SpecialEquipmentCode { get; private set; }
    public string PutAwayTemplateCode { get; private set; }
    public string PutAwayUnitOfMeasureCode { get; private set; }
    public string PhysInvtCountingPeriodCode { get; private set; }
    public DateTime? LastCountingPeriodUpdate { get; private set; }
    public bool UseCrossDocking { get; private set; }
    public DateTime? NextCountingStartDate { get; private set; }
    public DateTime? NextCountingEndDate { get; private set; }

    public static OperationResult<StockkeepingUnit, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StockkeepingUnit, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StockkeepingUnit()
        {
            TenantId = tenantId
        };
        return OperationResult<StockkeepingUnit, DomainError>.Ok(entity);
    }
}
