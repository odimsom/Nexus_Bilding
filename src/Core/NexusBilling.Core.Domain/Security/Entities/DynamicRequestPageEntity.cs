using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class DynamicRequestPageEntity : Entity
{
    private DynamicRequestPageEntity() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int TableId { get; private set; }
    public int RelatedTableId { get; private set; }
    public int SequenceNo { get; private set; }

    public static OperationResult<DynamicRequestPageEntity, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DynamicRequestPageEntity, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DynamicRequestPageEntity()
        {
            TenantId = tenantId
        };
        return OperationResult<DynamicRequestPageEntity, DomainError>.Ok(entity);
    }
}
