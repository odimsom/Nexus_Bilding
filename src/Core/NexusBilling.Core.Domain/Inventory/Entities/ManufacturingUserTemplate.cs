using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ManufacturingUserTemplate : Entity
{
    private ManufacturingUserTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short CreatePurchaseOrder { get; private set; }
    public short CreateProductionOrder { get; private set; }
    public short CreateTransferOrder { get; private set; }
    public short CreateAssemblyOrder { get; private set; }
    public string PurchaseReqWkshTemplate { get; private set; }
    public string PurchaseWkshName { get; private set; }
    public string ProdReqWkshTemplate { get; private set; }
    public string ProdWkshName { get; private set; }
    public string TransferReqWkshTemplate { get; private set; }
    public string TransferWkshName { get; private set; }
    public short MakeOrders { get; private set; }

    public static OperationResult<ManufacturingUserTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ManufacturingUserTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ManufacturingUserTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<ManufacturingUserTemplate, DomainError>.Ok(entity);
    }
}
