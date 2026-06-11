using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserGroupPermissionSet : Entity
{
    private UserGroupPermissionSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserGroupCode { get; private set; }
    public string RoleId { get; private set; }
    public Guid AppId { get; private set; }
    public short Scope { get; private set; }

    public static OperationResult<UserGroupPermissionSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserGroupPermissionSet, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserGroupPermissionSet()
        {
            TenantId = tenantId
        };
        return OperationResult<UserGroupPermissionSet, DomainError>.Ok(entity);
    }
}
