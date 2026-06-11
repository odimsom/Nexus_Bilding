using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobUsageLink : Entity
{
    private JobUsageLink() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public int LineNo { get; private set; }
    public int EntryNo { get; private set; }

    public static OperationResult<JobUsageLink, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobUsageLink, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobUsageLink()
        {
            TenantId = tenantId
        };
        return OperationResult<JobUsageLink, DomainError>.Ok(entity);
    }
}
