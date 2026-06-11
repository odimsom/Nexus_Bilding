using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FaultCode : Entity
{
    private FaultCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string SymptomCode { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<FaultCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaultCode, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaultCode()
        {
            TenantId = tenantId
        };
        return OperationResult<FaultCode, DomainError>.Ok(entity);
    }
}
