using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Dimension : Entity
{
    private Dimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string CodeCaption { get; private set; }
    public string FilterCaption { get; private set; }
    public string Description { get; private set; }
    public bool Blocked { get; private set; }
    public string ConsolidationCode { get; private set; }
    public string MapToIcDimensionCode { get; private set; }

    public static OperationResult<Dimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Dimension, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Dimension()
        {
            TenantId = tenantId
        };
        return OperationResult<Dimension, DomainError>.Ok(entity);
    }
}
