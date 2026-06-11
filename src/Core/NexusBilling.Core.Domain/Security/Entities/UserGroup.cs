using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserGroup : Entity
{
    private UserGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string DefaultProfileId { get; private set; }
    public bool AssignToAllNewUsers { get; private set; }

    public static OperationResult<UserGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserGroup, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<UserGroup, DomainError>.Ok(entity);
    }
}
