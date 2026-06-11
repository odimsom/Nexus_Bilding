using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseShipmentHeader : Entity
{
    private WarehouseShipmentHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string AssignedUserId { get; private set; }
    public DateTime? AssignmentDate { get; private set; }
    public string AssignmentTime { get; private set; }
    public short SortingMethod { get; private set; }
    public string NoSeries { get; private set; }
    public string BinCode { get; private set; }
    public string ZoneCode { get; private set; }
    public short DocumentStatus { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public short Status { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public bool CreatePostedHeader { get; private set; }
    public string ShippingNo { get; private set; }
    public string LastShippingNo { get; private set; }
    public string ShippingNoSeries { get; private set; }

    public static OperationResult<WarehouseShipmentHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseShipmentHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseShipmentHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseShipmentHeader, DomainError>.Ok(entity);
    }
}
