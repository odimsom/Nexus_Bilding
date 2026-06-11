using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class RoutingLine : Entity
{
    private RoutingLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string RoutingNo { get; private set; }
    public string VersionCode { get; private set; }
    public string OperationNo { get; private set; }
    public string NextOperationNo { get; private set; }
    public string PreviousOperationNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string WorkCenterNo { get; private set; }
    public string WorkCenterGroupCode { get; private set; }
    public string Description { get; private set; }
    public decimal SetupTime { get; private set; }
    public decimal RunTime { get; private set; }
    public decimal WaitTime { get; private set; }
    public decimal MoveTime { get; private set; }
    public decimal FixedScrapQuantity { get; private set; }
    public decimal LotSize { get; private set; }
    public decimal ScrapFactor { get; private set; }
    public string SetupTimeUnitOfMeasCode { get; private set; }
    public string RunTimeUnitOfMeasCode { get; private set; }
    public string WaitTimeUnitOfMeasCode { get; private set; }
    public string MoveTimeUnitOfMeasCode { get; private set; }
    public decimal MinimumProcessTime { get; private set; }
    public decimal MaximumProcessTime { get; private set; }
    public decimal ConcurrentCapacities { get; private set; }
    public decimal SendAheadQuantity { get; private set; }
    public string RoutingLinkCode { get; private set; }
    public string StandardTaskCode { get; private set; }
    public decimal UnitCostPer { get; private set; }
    public bool Recalculate { get; private set; }
    public int SequenceNoForward { get; private set; }
    public int SequenceNoBackward { get; private set; }
    public decimal FixedScrapQtyAccum { get; private set; }
    public decimal ScrapFactorAccumulated { get; private set; }

    public static OperationResult<RoutingLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RoutingLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RoutingLine()
        {
            TenantId = tenantId
        };
        return OperationResult<RoutingLine, DomainError>.Ok(entity);
    }
}
