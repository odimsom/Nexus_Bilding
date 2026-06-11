using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ContactBusinessRelation : Entity
{
    private ContactBusinessRelation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ContactNo { get; private set; }
    public string BusinessRelationCode { get; private set; }
    public short LinkToTable { get; private set; }
    public string No { get; private set; }

    public static OperationResult<ContactBusinessRelation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ContactBusinessRelation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ContactBusinessRelation()
        {
            TenantId = tenantId
        };
        return OperationResult<ContactBusinessRelation, DomainError>.Ok(entity);
    }
}
