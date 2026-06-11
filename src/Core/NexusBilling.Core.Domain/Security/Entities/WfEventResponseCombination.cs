using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WfEventResponseCombination : Entity
{
    private WfEventResponseCombination() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string FunctionName { get; private set; }
    public short PredecessorType { get; private set; }
    public string PredecessorFunctionName { get; private set; }

    public static OperationResult<WfEventResponseCombination, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WfEventResponseCombination, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WfEventResponseCombination()
        {
            TenantId = tenantId
        };
        return OperationResult<WfEventResponseCombination, DomainError>.Ok(entity);
    }
}
