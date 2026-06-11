using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class UnitOfMeasure : Entity
{
    private UnitOfMeasure() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string InternationalStandardCode { get; private set; }

    public static OperationResult<UnitOfMeasure, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UnitOfMeasure, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UnitOfMeasure()
        {
            TenantId = tenantId
        };
        return OperationResult<UnitOfMeasure, DomainError>.Ok(entity);
    }
}
