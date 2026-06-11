using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class AccessControl : Entity
{
    private AccessControl() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public string RoleId { get; private set; }
    public string CompanyName { get; private set; }
    public short Scope { get; private set; }
    public Guid AppId { get; private set; }

    public static OperationResult<AccessControl, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccessControl, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccessControl()
        {
            TenantId = tenantId
        };
        return OperationResult<AccessControl, DomainError>.Ok(entity);
    }
}
