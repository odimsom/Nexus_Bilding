using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceCost : Entity
{
    private ServiceCost() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string AccountNo { get; private set; }
    public decimal DefaultUnitPrice { get; private set; }
    public decimal DefaultQuantity { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public short CostType { get; private set; }
    public string ServiceZoneCode { get; private set; }
    public decimal DefaultUnitCost { get; private set; }

    public static OperationResult<ServiceCost, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceCost, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceCost()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceCost, DomainError>.Ok(entity);
    }
}
