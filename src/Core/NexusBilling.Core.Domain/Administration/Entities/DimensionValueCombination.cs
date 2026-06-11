using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionValueCombination : Entity
{
    private DimensionValueCombination() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Dimension1Code { get; private set; }
    public string Dimension1ValueCode { get; private set; }
    public string Dimension2Code { get; private set; }
    public string Dimension2ValueCode { get; private set; }

    public static OperationResult<DimensionValueCombination, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionValueCombination, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionValueCombination()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionValueCombination, DomainError>.Ok(entity);
    }
}
