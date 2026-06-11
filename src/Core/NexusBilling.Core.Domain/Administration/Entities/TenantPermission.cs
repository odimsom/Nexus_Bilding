using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantPermission : Entity
{
    private TenantPermission() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid AppId { get; private set; }
    public string RoleId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public short ReadPermission { get; private set; }
    public short InsertPermission { get; private set; }
    public short ModifyPermission { get; private set; }
    public short DeletePermission { get; private set; }
    public short ExecutePermission { get; private set; }
    public string SecurityFilter { get; private set; }

    public static OperationResult<TenantPermission, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantPermission, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantPermission()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantPermission, DomainError>.Ok(entity);
    }
}
