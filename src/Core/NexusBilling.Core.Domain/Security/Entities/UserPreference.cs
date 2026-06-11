using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserPreference : Entity
{
    private UserPreference() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string InstructionCode { get; private set; }
    public byte[]? UserSelection { get; private set; }

    public static OperationResult<UserPreference, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserPreference, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserPreference()
        {
            TenantId = tenantId
        };
        return OperationResult<UserPreference, DomainError>.Ok(entity);
    }
}
