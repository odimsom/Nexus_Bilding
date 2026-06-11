using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class O365EmailSetup : Entity
{
    private O365EmailSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Email { get; private set; }
    public short Recipienttype { get; private set; }

    public static OperationResult<O365EmailSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<O365EmailSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new O365EmailSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<O365EmailSetup, DomainError>.Ok(entity);
    }
}
