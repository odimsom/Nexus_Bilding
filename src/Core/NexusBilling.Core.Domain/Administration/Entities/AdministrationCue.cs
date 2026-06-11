using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AdministrationCue : Entity
{
    private AdministrationCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<AdministrationCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AdministrationCue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AdministrationCue()
        {
            TenantId = tenantId
        };
        return OperationResult<AdministrationCue, DomainError>.Ok(entity);
    }
}
