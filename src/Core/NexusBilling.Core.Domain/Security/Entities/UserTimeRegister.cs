using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserTimeRegister : Entity
{
    private UserTimeRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public DateTime? Date { get; private set; }
    public decimal Minutes { get; private set; }

    public static OperationResult<UserTimeRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserTimeRegister, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserTimeRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<UserTimeRegister, DomainError>.Ok(entity);
    }
}
