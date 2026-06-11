using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DefaultDimensionPriority : Entity
{
    private DefaultDimensionPriority() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SourceCode { get; private set; }
    public int TableId { get; private set; }
    public int Priority { get; private set; }

    public static OperationResult<DefaultDimensionPriority, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DefaultDimensionPriority, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DefaultDimensionPriority()
        {
            TenantId = tenantId
        };
        return OperationResult<DefaultDimensionPriority, DomainError>.Ok(entity);
    }
}
