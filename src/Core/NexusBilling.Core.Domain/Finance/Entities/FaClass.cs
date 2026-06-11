using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaClass : Entity
{
    private FaClass() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<FaClass, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaClass, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaClass()
        {
            TenantId = tenantId
        };
        return OperationResult<FaClass, DomainError>.Ok(entity);
    }
}
