using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantPermissionSet : Entity
{
    private TenantPermissionSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid AppId { get; private set; }
    public string RoleId { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<TenantPermissionSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantPermissionSet, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantPermissionSet()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantPermissionSet, DomainError>.Ok(entity);
    }
}
