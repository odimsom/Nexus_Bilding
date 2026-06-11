using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BaseCalendar : Entity
{
    private BaseCalendar() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<BaseCalendar, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BaseCalendar, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BaseCalendar()
        {
            TenantId = tenantId
        };
        return OperationResult<BaseCalendar, DomainError>.Ok(entity);
    }
}
