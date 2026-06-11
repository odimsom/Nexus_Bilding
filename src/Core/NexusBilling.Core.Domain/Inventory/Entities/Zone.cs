using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class Zone : Entity
{
    private Zone() { }

    public TenantIdentifier TenantId { get; private set; }
    public string LocationCode { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string BinTypeCode { get; private set; }
    public string WarehouseClassCode { get; private set; }
    public string SpecialEquipmentCode { get; private set; }
    public int ZoneRanking { get; private set; }
    public bool CrossDockBinZone { get; private set; }

    public static OperationResult<Zone, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Zone, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Zone()
        {
            TenantId = tenantId
        };
        return OperationResult<Zone, DomainError>.Ok(entity);
    }
}
