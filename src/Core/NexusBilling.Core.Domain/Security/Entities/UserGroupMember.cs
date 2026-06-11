using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserGroupMember : Entity
{
    private UserGroupMember() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserGroupCode { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public string CompanyName { get; private set; }

    public static OperationResult<UserGroupMember, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserGroupMember, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserGroupMember()
        {
            TenantId = tenantId
        };
        return OperationResult<UserGroupMember, DomainError>.Ok(entity);
    }
}
