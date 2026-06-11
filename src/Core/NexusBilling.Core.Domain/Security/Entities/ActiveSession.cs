using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class ActiveSession : Entity
{
    private ActiveSession() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public int ServerInstanceId { get; private set; }
    public int SessionId { get; private set; }
    public string ServerInstanceName { get; private set; }
    public string ServerComputerName { get; private set; }
    public string UserId { get; private set; }
    public short ClientType { get; private set; }
    public string ClientComputerName { get; private set; }
    public DateTime? LoginDatetime { get; private set; }
    public string DatabaseName { get; private set; }
    public Guid SessionUniqueId { get; private set; }

    public static OperationResult<ActiveSession, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ActiveSession, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ActiveSession()
        {
            TenantId = tenantId
        };
        return OperationResult<ActiveSession, DomainError>.Ok(entity);
    }
}
