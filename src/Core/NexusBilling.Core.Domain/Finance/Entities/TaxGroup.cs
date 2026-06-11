using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TaxGroup : Entity
{
    private TaxGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<TaxGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TaxGroup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TaxGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<TaxGroup, DomainError>.Ok(entity);
    }
}
