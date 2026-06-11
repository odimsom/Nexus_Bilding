using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserSecurityStatus : Entity
{
    private UserSecurityStatus() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public bool Reviewed { get; private set; }

    public static OperationResult<UserSecurityStatus, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserSecurityStatus, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserSecurityStatus()
        {
            TenantId = tenantId
        };
        return OperationResult<UserSecurityStatus, DomainError>.Ok(entity);
    }
}
