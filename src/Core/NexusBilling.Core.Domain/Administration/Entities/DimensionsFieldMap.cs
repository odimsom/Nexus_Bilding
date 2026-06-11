using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionsFieldMap : Entity
{
    private DimensionsFieldMap() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableNo { get; private set; }
    public int GlobalDim1FieldNo { get; private set; }
    public int GlobalDim2FieldNo { get; private set; }
    public int IdFieldNo { get; private set; }

    public static OperationResult<DimensionsFieldMap, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionsFieldMap, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionsFieldMap()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionsFieldMap, DomainError>.Ok(entity);
    }
}
