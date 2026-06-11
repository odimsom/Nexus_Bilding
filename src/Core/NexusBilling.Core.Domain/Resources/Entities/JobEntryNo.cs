using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobEntryNo : Entity
{
    private JobEntryNo() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public int EntryNo { get; private set; }

    public static OperationResult<JobEntryNo, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobEntryNo, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobEntryNo()
        {
            TenantId = tenantId
        };
        return OperationResult<JobEntryNo, DomainError>.Ok(entity);
    }
}
