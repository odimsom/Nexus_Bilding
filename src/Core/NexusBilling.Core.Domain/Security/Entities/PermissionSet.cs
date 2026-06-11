using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class PermissionSet : Entity
{
    private PermissionSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public string RoleId { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<PermissionSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PermissionSet, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PermissionSet()
        {
            TenantId = tenantId
        };
        return OperationResult<PermissionSet, DomainError>.Ok(entity);
    }
}
