using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NotificationSchedule : Entity
{
    private NotificationSchedule() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short NotificationType { get; private set; }
    public short Recurrence { get; private set; }
    public string Time { get; private set; }
    public short DailyFrequency { get; private set; }
    public bool Monday { get; private set; }
    public bool Tuesday { get; private set; }
    public bool Wednesday { get; private set; }
    public bool Thursday { get; private set; }
    public bool Friday { get; private set; }
    public bool Saturday { get; private set; }
    public bool Sunday { get; private set; }
    public int DateOfMonth { get; private set; }
    public short MonthlyNotificationDate { get; private set; }
    public Guid LastScheduledJob { get; private set; }

    public static OperationResult<NotificationSchedule, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NotificationSchedule, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NotificationSchedule()
        {
            TenantId = tenantId
        };
        return OperationResult<NotificationSchedule, DomainError>.Ok(entity);
    }
}
