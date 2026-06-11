using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankClearingStandard : Entity
{
    private BankClearingStandard() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<BankClearingStandard, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankClearingStandard, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankClearingStandard()
        {
            TenantId = tenantId
        };
        return OperationResult<BankClearingStandard, DomainError>.Ok(entity);
    }
}
