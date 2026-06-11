using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionSetTreeNode : Entity
{
    private DimensionSetTreeNode() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ParentDimensionSetId { get; private set; }
    public int DimensionValueId { get; private set; }
    public int DimensionSetId { get; private set; }
    public bool InUse { get; private set; }

    public static OperationResult<DimensionSetTreeNode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionSetTreeNode, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionSetTreeNode()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionSetTreeNode, DomainError>.Ok(entity);
    }
}
