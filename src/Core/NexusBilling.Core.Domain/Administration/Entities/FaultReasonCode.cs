using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FaultReasonCode : Entity
{
    private FaultReasonCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool ExcludeWarrantyDiscount { get; private set; }
    public bool ExcludeContractDiscount { get; private set; }

    public static OperationResult<FaultReasonCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaultReasonCode, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaultReasonCode()
        {
            TenantId = tenantId
        };
        return OperationResult<FaultReasonCode, DomainError>.Ok(entity);
    }
}
