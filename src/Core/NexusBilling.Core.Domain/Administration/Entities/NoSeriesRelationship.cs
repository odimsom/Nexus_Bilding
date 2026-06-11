using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NoSeriesRelationship : Entity
{
    private NoSeriesRelationship() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string SeriesCode { get; private set; }

    public static OperationResult<NoSeriesRelationship, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NoSeriesRelationship, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NoSeriesRelationship()
        {
            TenantId = tenantId
        };
        return OperationResult<NoSeriesRelationship, DomainError>.Ok(entity);
    }
}
