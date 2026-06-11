using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class ReqWkshTemplate : Entity
{
    private ReqWkshTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int PageId { get; private set; }
    public bool Recurring { get; private set; }
    public short Type { get; private set; }

    public static OperationResult<ReqWkshTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReqWkshTemplate, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReqWkshTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<ReqWkshTemplate, DomainError>.Ok(entity);
    }
}
