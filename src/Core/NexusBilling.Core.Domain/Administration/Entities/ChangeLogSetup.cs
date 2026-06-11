using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ChangeLogSetup : Entity
{
    private ChangeLogSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public bool ChangeLogActivated { get; private set; }

    public static OperationResult<ChangeLogSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ChangeLogSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ChangeLogSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ChangeLogSetup, DomainError>.Ok(entity);
    }
}
