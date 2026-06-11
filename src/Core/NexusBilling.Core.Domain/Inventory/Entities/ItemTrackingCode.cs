using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemTrackingCode : Entity
{
    private ItemTrackingCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string WarrantyDateFormula { get; private set; }
    public bool ManWarrantyDateEntryReqd { get; private set; }
    public bool ManExpirDateEntryReqd { get; private set; }
    public bool StrictExpirationPosting { get; private set; }
    public bool SnSpecificTracking { get; private set; }
    public bool SnInfoInboundMustExist { get; private set; }
    public bool SnInfoOutboundMustExist { get; private set; }
    public bool SnWarehouseTracking { get; private set; }
    public bool SnPurchaseInboundTracking { get; private set; }
    public bool SnPurchaseOutboundTracking { get; private set; }
    public bool SnSalesInboundTracking { get; private set; }
    public bool SnSalesOutboundTracking { get; private set; }
    public bool SnPosAdjmtInbTracking { get; private set; }
    public bool SnPosAdjmtOutbTracking { get; private set; }
    public bool SnNegAdjmtInbTracking { get; private set; }
    public bool SnNegAdjmtOutbTracking { get; private set; }
    public bool SnTransferTracking { get; private set; }
    public bool SnManufInboundTracking { get; private set; }
    public bool SnManufOutboundTracking { get; private set; }
    public bool SnAssemblyInboundTracking { get; private set; }
    public bool SnAssemblyOutboundTracking { get; private set; }
    public bool LotSpecificTracking { get; private set; }
    public bool LotInfoInboundMustExist { get; private set; }
    public bool LotInfoOutboundMustExist { get; private set; }
    public bool LotWarehouseTracking { get; private set; }
    public bool LotPurchaseInboundTracking { get; private set; }
    public bool LotPurchaseOutboundTracking { get; private set; }
    public bool LotSalesInboundTracking { get; private set; }
    public bool LotSalesOutboundTracking { get; private set; }
    public bool LotPosAdjmtInbTracking { get; private set; }
    public bool LotPosAdjmtOutbTracking { get; private set; }
    public bool LotNegAdjmtInbTracking { get; private set; }
    public bool LotNegAdjmtOutbTracking { get; private set; }
    public bool LotTransferTracking { get; private set; }
    public bool LotManufInboundTracking { get; private set; }
    public bool LotManufOutboundTracking { get; private set; }
    public bool LotAssemblyInboundTracking { get; private set; }
    public bool LotAssemblyOutboundTracking { get; private set; }

    public static OperationResult<ItemTrackingCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemTrackingCode, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemTrackingCode()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemTrackingCode, DomainError>.Ok(entity);
    }
}
