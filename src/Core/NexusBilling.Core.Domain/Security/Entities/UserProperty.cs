using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserProperty : Entity
{
    private UserProperty() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public string Password { get; private set; }
    public string NameIdentifier { get; private set; }
    public string AuthenticationKey { get; private set; }
    public string WebservicesKey { get; private set; }
    public DateTime? WebservicesKeyExpiryDate { get; private set; }
    public string AuthenticationObjectId { get; private set; }

    public static OperationResult<UserProperty, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserProperty, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserProperty()
        {
            TenantId = tenantId
        };
        return OperationResult<UserProperty, DomainError>.Ok(entity);
    }
}
