using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseSetup : Entity
{
    private WarehouseSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string WhseReceiptNos { get; private set; }
    public string WhsePutAwayNos { get; private set; }
    public string WhsePickNos { get; private set; }
    public string WhseShipNos { get; private set; }
    public string RegisteredWhsePickNos { get; private set; }
    public string RegisteredWhsePutAwayNos { get; private set; }
    public bool RequireReceive { get; private set; }
    public bool RequirePutAway { get; private set; }
    public bool RequirePick { get; private set; }
    public bool RequireShipment { get; private set; }
    public int LastWhsePostingRefNo { get; private set; }
    public short ReceiptPostingPolicy { get; private set; }
    public short ShipmentPostingPolicy { get; private set; }
    public string PostedWhseReceiptNos { get; private set; }
    public string PostedWhseShipmentNos { get; private set; }
    public string WhseInternalPutAwayNos { get; private set; }
    public string WhseInternalPickNos { get; private set; }
    public string WhseMovementNos { get; private set; }
    public string RegisteredWhseMovementNos { get; private set; }

    public static OperationResult<WarehouseSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseSetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseSetup, DomainError>.Ok(entity);
    }
}
