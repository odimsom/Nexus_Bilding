using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MyNotifications : Entity
{
    private MyNotifications() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public Guid NotificationId { get; private set; }
    public int ApplyToTableId { get; private set; }
    public bool Enabled { get; private set; }
    public byte[]? ApplyToTableFilter { get; private set; }
    public string Name { get; private set; }
    public byte[]? Description { get; private set; }

    public static OperationResult<MyNotifications, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MyNotifications, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MyNotifications()
        {
            TenantId = tenantId
        };
        return OperationResult<MyNotifications, DomainError>.Ok(entity);
    }
}
