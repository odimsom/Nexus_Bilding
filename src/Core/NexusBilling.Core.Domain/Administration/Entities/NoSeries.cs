using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NoSeries : Entity
{
    private NoSeries() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool DefaultNos { get; private set; }
    public bool ManualNos { get; private set; }
    public bool DateOrder { get; private set; }

    public static OperationResult<NoSeries, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NoSeries, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NoSeries()
        {
            TenantId = tenantId
        };
        return OperationResult<NoSeries, DomainError>.Ok(entity);
    }
}
