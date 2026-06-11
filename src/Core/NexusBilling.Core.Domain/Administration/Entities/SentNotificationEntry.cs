using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SentNotificationEntry : Entity
{
    private SentNotificationEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public short Type { get; private set; }
    public string RecipientUserId { get; private set; }
    public string TriggeredByRecord { get; private set; }
    public int LinkTargetPage { get; private set; }
    public string CustomLink { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime? SentDateTime { get; private set; }
    public byte[]? NotificationContent { get; private set; }
    public short NotificationMethod { get; private set; }
    public int AggregatedWithEntry { get; private set; }

    public static OperationResult<SentNotificationEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SentNotificationEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SentNotificationEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<SentNotificationEntry, DomainError>.Ok(entity);
    }
}
