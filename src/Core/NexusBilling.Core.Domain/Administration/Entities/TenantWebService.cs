using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantWebService : Entity
{
    private TenantWebService() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public string ServiceName { get; private set; }
    public bool Published { get; private set; }

    public static OperationResult<TenantWebService, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantWebService, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantWebService()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantWebService, DomainError>.Ok(entity);
    }
}
