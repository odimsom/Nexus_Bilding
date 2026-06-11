using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseRequest : Entity
{
    private WarehouseRequest() { }

    public TenantIdentifier TenantId { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceNo { get; private set; }
    public short SourceDocument { get; private set; }
    public short DocumentStatus { get; private set; }
    public string LocationCode { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public short ShippingAdvice { get; private set; }
    public short DestinationType { get; private set; }
    public string DestinationNo { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public short Type { get; private set; }
    public bool CompletelyHandled { get; private set; }

    public static OperationResult<WarehouseRequest, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseRequest, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseRequest()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseRequest, DomainError>.Ok(entity);
    }
}
