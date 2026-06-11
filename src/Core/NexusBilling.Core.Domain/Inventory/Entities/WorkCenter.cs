using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WorkCenter : Entity
{
    private WorkCenter() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string AlternateWorkCenter { get; private set; }
    public string WorkCenterGroupCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string SubcontractorNo { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal QueueTime { get; private set; }
    public string QueueTimeUnitOfMeasCode { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal Capacity { get; private set; }
    public decimal Efficiency { get; private set; }
    public decimal MaximumEfficiency { get; private set; }
    public decimal MinimumEfficiency { get; private set; }
    public decimal CalendarRoundingPrecision { get; private set; }
    public short SimulationType { get; private set; }
    public string ShopCalendarCode { get; private set; }
    public bool Blocked { get; private set; }
    public short UnitCostCalculation { get; private set; }
    public bool SpecificUnitCost { get; private set; }
    public bool ConsolidatedCalendar { get; private set; }
    public short FlushingMethod { get; private set; }
    public string NoSeries { get; private set; }
    public decimal OverheadRate { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string County { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string LocationCode { get; private set; }
    public string OpenShopFloorBinCode { get; private set; }
    public string ToProductionBinCode { get; private set; }
    public string FromProductionBinCode { get; private set; }

    public static OperationResult<WorkCenter, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkCenter, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkCenter()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkCenter, DomainError>.Ok(entity);
    }
}
