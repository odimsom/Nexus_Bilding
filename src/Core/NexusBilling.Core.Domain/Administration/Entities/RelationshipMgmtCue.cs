using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class RelationshipMgmtCue : Entity
{
    private RelationshipMgmtCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<RelationshipMgmtCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RelationshipMgmtCue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RelationshipMgmtCue()
        {
            TenantId = tenantId
        };
        return OperationResult<RelationshipMgmtCue, DomainError>.Ok(entity);
    }
}
