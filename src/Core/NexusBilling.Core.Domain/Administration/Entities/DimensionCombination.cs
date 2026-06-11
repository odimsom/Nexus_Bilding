using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionCombination : Entity
{
    private DimensionCombination() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Dimension1Code { get; private set; }
    public string Dimension2Code { get; private set; }
    public short CombinationRestriction { get; private set; }

    public static OperationResult<DimensionCombination, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionCombination, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionCombination()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionCombination, DomainError>.Ok(entity);
    }
}
