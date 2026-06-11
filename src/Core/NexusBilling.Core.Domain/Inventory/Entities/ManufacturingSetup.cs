using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ManufacturingSetup : Entity
{
    private ManufacturingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string NormalStartingTime { get; private set; }
    public string NormalEndingTime { get; private set; }
    public bool DocNoIsProdOrderNo { get; private set; }
    public bool CostInclSetup { get; private set; }
    public bool DynamicLowLevelCode { get; private set; }
    public bool PlanningWarning { get; private set; }
    public string SimulatedOrderNos { get; private set; }
    public string PlannedOrderNos { get; private set; }
    public string FirmPlannedOrderNos { get; private set; }
    public string ReleasedOrderNos { get; private set; }
    public string WorkCenterNos { get; private set; }
    public string MachineCenterNos { get; private set; }
    public string ProductionBomNos { get; private set; }
    public string RoutingNos { get; private set; }
    public string CurrentProductionForecast { get; private set; }
    public bool UseForecastOnLocations { get; private set; }
    public bool CombinedMpsMrpCalculation { get; private set; }
    public string ComponentsAtLocation { get; private set; }
    public string DefaultDampenerPeriod { get; private set; }
    public decimal DefaultDampener { get; private set; }
    public string DefaultSafetyLeadTime { get; private set; }
    public short BlankOverflowLevel { get; private set; }
    public string ShowCapacityIn { get; private set; }
    public short PresetOutputQuantity { get; private set; }

    public static OperationResult<ManufacturingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ManufacturingSetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ManufacturingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ManufacturingSetup, DomainError>.Ok(entity);
    }
}
