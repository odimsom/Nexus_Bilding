using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobQueueEntry : Entity
{
    private JobQueueEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public string UserId { get; private set; }
    public byte[]? Xml { get; private set; }
    public DateTime? LastReadyState { get; private set; }
    public DateTime? ExpirationDateTime { get; private set; }
    public DateTime? EarliestStartDateTime { get; private set; }
    public short ObjectTypeToRun { get; private set; }
    public int ObjectIdToRun { get; private set; }
    public short ReportOutputType { get; private set; }
    public int MaximumNoOfAttemptsToRun { get; private set; }
    public int NoOfAttemptsToRun { get; private set; }
    public short Status { get; private set; }
    public int Priority { get; private set; }
    public string RecordIdToProcess { get; private set; }
    public string ParameterString { get; private set; }
    public bool RecurringJob { get; private set; }
    public int NoOfMinutesBetweenRuns { get; private set; }
    public bool RunOnMondays { get; private set; }
    public bool RunOnTuesdays { get; private set; }
    public bool RunOnWednesdays { get; private set; }
    public bool RunOnThursdays { get; private set; }
    public bool RunOnFridays { get; private set; }
    public bool RunOnSaturdays { get; private set; }
    public bool RunOnSundays { get; private set; }
    public string StartingTime { get; private set; }
    public string EndingTime { get; private set; }
    public DateTime? ReferenceStartingTime { get; private set; }
    public string Description { get; private set; }
    public bool RunInUserSession { get; private set; }
    public int UserSessionId { get; private set; }
    public string JobQueueCategoryCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public string ErrorMessage2 { get; private set; }
    public string ErrorMessage3 { get; private set; }
    public string ErrorMessage4 { get; private set; }
    public int UserServiceInstanceId { get; private set; }
    public DateTime? UserSessionStarted { get; private set; }
    public int TimeoutSec { get; private set; }
    public bool NotifyOnSuccess { get; private set; }
    public int UserLanguageId { get; private set; }
    public string PrinterName { get; private set; }
    public bool ReportRequestPageOptions { get; private set; }
    public int RerunDelaySec { get; private set; }
    public Guid SystemTaskId { get; private set; }

    public static OperationResult<JobQueueEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobQueueEntry, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobQueueEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<JobQueueEntry, DomainError>.Ok(entity);
    }
}
