using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ScheduledTask : Entity
{
    private ScheduledTask() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public int UserLanguageId { get; private set; }
    public int UserFormatId { get; private set; }
    public string UserTimeZone { get; private set; }
    public string Company { get; private set; }
    public bool IsReady { get; private set; }
    public DateTime? NotBefore { get; private set; }
    public int RunCodeunit { get; private set; }
    public int FailureCodeunit { get; private set; }
    public string Record { get; private set; }

    public static OperationResult<ScheduledTask, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ScheduledTask, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ScheduledTask()
        {
            TenantId = tenantId
        };
        return OperationResult<ScheduledTask, DomainError>.Ok(entity);
    }
}
