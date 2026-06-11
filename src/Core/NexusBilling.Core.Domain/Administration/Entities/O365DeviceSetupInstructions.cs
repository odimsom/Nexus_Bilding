using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class O365DeviceSetupInstructions : Entity
{
    private O365DeviceSetupInstructions() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Key { get; private set; }
    public string SetupUrl { get; private set; }
    public byte[]? QrCode { get; private set; }

    public static OperationResult<O365DeviceSetupInstructions, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<O365DeviceSetupInstructions, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new O365DeviceSetupInstructions()
        {
            TenantId = tenantId
        };
        return OperationResult<O365DeviceSetupInstructions, DomainError>.Ok(entity);
    }
}
