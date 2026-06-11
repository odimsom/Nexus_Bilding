using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OnlineMapSetup : Entity
{
    private OnlineMapSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string MapParameterSetupCode { get; private set; }
    public short DistanceIn { get; private set; }
    public short Route { get; private set; }

    public static OperationResult<OnlineMapSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OnlineMapSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OnlineMapSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OnlineMapSetup, DomainError>.Ok(entity);
    }
}
