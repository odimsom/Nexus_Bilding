using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NotificationSetup : Entity
{
    private NotificationSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short NotificationType { get; private set; }
    public short NotificationMethod { get; private set; }
    public short DisplayTarget { get; private set; }

    public static OperationResult<NotificationSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NotificationSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NotificationSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<NotificationSetup, DomainError>.Ok(entity);
    }
}
