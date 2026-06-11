using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class WebService : Entity
{
    private WebService() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public string ServiceName { get; private set; }
    public bool Published { get; private set; }

    public static OperationResult<WebService, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WebService, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WebService()
        {
            TenantId = tenantId
        };
        return OperationResult<WebService, DomainError>.Ok(entity);
    }
}
