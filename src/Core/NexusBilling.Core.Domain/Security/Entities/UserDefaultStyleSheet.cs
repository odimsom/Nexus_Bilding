using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserDefaultStyleSheet : Entity
{
    private UserDefaultStyleSheet() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public Guid ProgramId { get; private set; }
    public Guid StyleSheetId { get; private set; }

    public static OperationResult<UserDefaultStyleSheet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserDefaultStyleSheet, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserDefaultStyleSheet()
        {
            TenantId = tenantId
        };
        return OperationResult<UserDefaultStyleSheet, DomainError>.Ok(entity);
    }
}
