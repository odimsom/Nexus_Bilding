using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class SessionEvent : Entity
{
    private SessionEvent() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public int ServerInstanceId { get; private set; }
    public int SessionId { get; private set; }
    public short EventType { get; private set; }
    public DateTime? EventDatetime { get; private set; }
    public short ClientType { get; private set; }
    public string DatabaseName { get; private set; }
    public string ClientComputerName { get; private set; }
    public string UserId { get; private set; }
    public string Comment { get; private set; }
    public Guid SessionUniqueId { get; private set; }

    public static OperationResult<SessionEvent, DomainError> Create(
        TenantIdentifier tenantId,
        Guid userSid,
        short eventType,
        string userId,
        Guid sessionUniqueId,
        string comment = "")
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SessionEvent, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SessionEvent()
        {
            TenantId = tenantId,
            UserSid = userSid,
            EventType = eventType,
            EventDatetime = DateTime.UtcNow,
            UserId = userId,
            SessionUniqueId = sessionUniqueId,
            Comment = comment,
            DatabaseName = "nexus_db",
            ServerInstanceId = 1,
            ClientComputerName = "web-client"
        };
        return OperationResult<SessionEvent, DomainError>.Ok(entity);
    }
}
