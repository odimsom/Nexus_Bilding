using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NotificationEntry : Entity
{
    private NotificationEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public short Type { get; private set; }
    public string RecipientUserId { get; private set; }
    public string TriggeredByRecord { get; private set; }
    public int LinkTargetPage { get; private set; }
    public string CustomLink { get; private set; }
    public string ErrorMessage { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public string CreatedBy { get; private set; }
    public string ErrorMessage2 { get; private set; }
    public string ErrorMessage3 { get; private set; }
    public string ErrorMessage4 { get; private set; }

    public static OperationResult<NotificationEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NotificationEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NotificationEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<NotificationEntry, DomainError>.Ok(entity);
    }
}
