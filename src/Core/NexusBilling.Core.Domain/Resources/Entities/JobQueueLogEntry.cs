using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobQueueLogEntry : Entity
{
    private JobQueueLogEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public Guid IdNav { get; private set; }
    public string UserId { get; private set; }
    public DateTime? StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public short ObjectTypeToRun { get; private set; }
    public int ObjectIdToRun { get; private set; }
    public short Status { get; private set; }
    public string Description { get; private set; }
    public string ErrorMessage { get; private set; }
    public string ErrorMessage2 { get; private set; }
    public string ErrorMessage3 { get; private set; }
    public string ErrorMessage4 { get; private set; }
    public string ProcessedByUserId { get; private set; }
    public string JobQueueCategoryCode { get; private set; }

    public static OperationResult<JobQueueLogEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobQueueLogEntry, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobQueueLogEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<JobQueueLogEntry, DomainError>.Ok(entity);
    }
}
