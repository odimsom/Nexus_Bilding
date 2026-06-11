using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class AdcsUser : Entity
{
    private AdcsUser() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }

    public static OperationResult<AdcsUser, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AdcsUser, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AdcsUser()
        {
            TenantId = tenantId
        };
        return OperationResult<AdcsUser, DomainError>.Ok(entity);
    }
}
