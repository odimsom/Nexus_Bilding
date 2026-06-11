using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionSetEntry : Entity
{
    private DimensionSetEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int DimensionSetId { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueCode { get; private set; }
    public int DimensionValueId { get; private set; }

    public static OperationResult<DimensionSetEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionSetEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionSetEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionSetEntry, DomainError>.Ok(entity);
    }
}
