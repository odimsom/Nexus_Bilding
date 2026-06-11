using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class Workflow : Entity
{
    private Workflow() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool Enabled { get; private set; }
    public bool Template { get; private set; }
    public string Category { get; private set; }

    public static OperationResult<Workflow, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Workflow, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Workflow()
        {
            TenantId = tenantId
        };
        return OperationResult<Workflow, DomainError>.Ok(entity);
    }
}
