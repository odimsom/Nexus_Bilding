using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class InventorySetup : Entity
{
    private InventorySetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public bool AutomaticCostPosting { get; private set; }
    public bool LocationMandatory { get; private set; }
    public string ItemNos { get; private set; }
    public short AutomaticCostAdjustment { get; private set; }
    public bool PreventNegativeInventory { get; private set; }
    public string TransferOrderNos { get; private set; }
    public string PostedTransferShptNos { get; private set; }
    public string PostedTransferRcptNos { get; private set; }
    public bool CopyCommentsOrderToShpt { get; private set; }
    public bool CopyCommentsOrderToRcpt { get; private set; }
    public string NonstockItemNos { get; private set; }
    public string OutboundWhseHandlingTime { get; private set; }
    public string InboundWhseHandlingTime { get; private set; }
    public bool ExpectedCostPostingToGL { get; private set; }
    public short AverageCostCalcType { get; private set; }
    public short AverageCostPeriod { get; private set; }
    public string ItemGroupDimensionCode { get; private set; }
    public string InventoryPutAwayNos { get; private set; }
    public string InventoryPickNos { get; private set; }
    public string PostedInvtPutAwayNos { get; private set; }
    public string PostedInvtPickNos { get; private set; }
    public string InventoryMovementNos { get; private set; }
    public string RegisteredInvtMovementNos { get; private set; }
    public string InternalMovementNos { get; private set; }

    public static OperationResult<InventorySetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InventorySetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InventorySetup()
        {
            TenantId = tenantId
        };
        return OperationResult<InventorySetup, DomainError>.Ok(entity);
    }
}
