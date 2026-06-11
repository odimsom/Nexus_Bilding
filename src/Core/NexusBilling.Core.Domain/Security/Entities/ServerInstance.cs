using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class ServerInstance : Entity
{
    private ServerInstance() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ServerInstanceId { get; private set; }
    public string ServiceName { get; private set; }
    public string ServerComputerName { get; private set; }
    public DateTime? LastActive { get; private set; }
    public string ServerInstanceName { get; private set; }
    public int ServerPort { get; private set; }
    public int ManagementPort { get; private set; }
    public short Status { get; private set; }

    public static OperationResult<ServerInstance, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServerInstance, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServerInstance()
        {
            TenantId = tenantId
        };
        return OperationResult<ServerInstance, DomainError>.Ok(entity);
    }
}
