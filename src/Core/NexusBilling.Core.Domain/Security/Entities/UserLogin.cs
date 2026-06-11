using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserLogin : Entity
{
    private UserLogin() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public DateTime? FirstLoginDate { get; private set; }

    public static OperationResult<UserLogin, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserLogin, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserLogin()
        {
            TenantId = tenantId
        };
        return OperationResult<UserLogin, DomainError>.Ok(entity);
    }
}
