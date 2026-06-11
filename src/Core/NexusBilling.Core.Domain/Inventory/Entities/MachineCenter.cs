using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class MachineCenter : Entity
{
    private MachineCenter() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string WorkCenterNo { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal QueueTime { get; private set; }
    public string QueueTimeUnitOfMeasCode { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public decimal Capacity { get; private set; }
    public decimal Efficiency { get; private set; }
    public decimal MaximumEfficiency { get; private set; }
    public decimal MinimumEfficiency { get; private set; }
    public bool Blocked { get; private set; }
    public decimal SetupTime { get; private set; }
    public decimal WaitTime { get; private set; }
    public decimal MoveTime { get; private set; }
    public decimal FixedScrapQuantity { get; private set; }
    public decimal Scrap { get; private set; }
    public string SetupTimeUnitOfMeasCode { get; private set; }
    public string WaitTimeUnitOfMeasCode { get; private set; }
    public decimal SendAheadQuantity { get; private set; }
    public string MoveTimeUnitOfMeasCode { get; private set; }
    public short FlushingMethod { get; private set; }
    public decimal MinimumProcessTime { get; private set; }
    public decimal MaximumProcessTime { get; private set; }
    public decimal ConcurrentCapacities { get; private set; }
    public string NoSeries { get; private set; }
    public decimal OverheadRate { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string County { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string LocationCode { get; private set; }
    public string OpenShopFloorBinCode { get; private set; }
    public string ToProductionBinCode { get; private set; }
    public string FromProductionBinCode { get; private set; }

    public static OperationResult<MachineCenter, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MachineCenter, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MachineCenter()
        {
            TenantId = tenantId
        };
        return OperationResult<MachineCenter, DomainError>.Ok(entity);
    }
}
