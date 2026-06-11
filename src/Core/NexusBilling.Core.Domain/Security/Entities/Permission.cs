using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class Permission : Entity
{
    private Permission() { }

    public TenantIdentifier TenantId { get; private set; }
    public string RoleId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public short ReadPermission { get; private set; }
    public short InsertPermission { get; private set; }
    public short ModifyPermission { get; private set; }
    public short DeletePermission { get; private set; }
    public short ExecutePermission { get; private set; }
    public string SecurityFilter { get; private set; }

    public static OperationResult<Permission, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Permission, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Permission()
        {
            TenantId = tenantId
        };
        return OperationResult<Permission, DomainError>.Ok(entity);
    }
}
