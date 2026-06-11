using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseEmployee : Entity
{
    private WarehouseEmployee() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string LocationCode { get; private set; }
    public bool Default { get; private set; }
    public string AdcsUser { get; private set; }

    public static OperationResult<WarehouseEmployee, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseEmployee, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseEmployee()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseEmployee, DomainError>.Ok(entity);
    }
}
