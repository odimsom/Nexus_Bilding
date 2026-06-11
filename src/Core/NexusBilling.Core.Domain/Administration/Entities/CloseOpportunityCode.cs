using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CloseOpportunityCode : Entity
{
    private CloseOpportunityCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short Type { get; private set; }

    public static OperationResult<CloseOpportunityCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CloseOpportunityCode, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CloseOpportunityCode()
        {
            TenantId = tenantId
        };
        return OperationResult<CloseOpportunityCode, DomainError>.Ok(entity);
    }
}
