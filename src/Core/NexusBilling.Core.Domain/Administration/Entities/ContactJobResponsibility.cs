using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ContactJobResponsibility : Entity
{
    private ContactJobResponsibility() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ContactNo { get; private set; }
    public string JobResponsibilityCode { get; private set; }

    public static OperationResult<ContactJobResponsibility, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ContactJobResponsibility, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ContactJobResponsibility()
        {
            TenantId = tenantId
        };
        return OperationResult<ContactJobResponsibility, DomainError>.Ok(entity);
    }
}
