using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ApplicationAreaSetup : Entity
{
    private ApplicationAreaSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CompanyName { get; private set; }
    public string ProfileId { get; private set; }
    public string UserId { get; private set; }
    public bool Basic { get; private set; }
    public bool Suite { get; private set; }
    public bool RelationshipMgmt { get; private set; }
    public bool Jobs { get; private set; }
    public bool FixedAssets { get; private set; }

    public static OperationResult<ApplicationAreaSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ApplicationAreaSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ApplicationAreaSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ApplicationAreaSetup, DomainError>.Ok(entity);
    }
}
