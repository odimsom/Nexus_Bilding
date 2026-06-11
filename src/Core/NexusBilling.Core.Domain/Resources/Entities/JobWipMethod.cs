using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobWipMethod : Entity
{
    private JobWipMethod() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool WipCost { get; private set; }
    public bool WipSales { get; private set; }
    public short RecognizedCosts { get; private set; }
    public short RecognizedSales { get; private set; }
    public bool Valid { get; private set; }
    public bool SystemDefined { get; private set; }
    public int SystemDefinedIndex { get; private set; }

    public static OperationResult<JobWipMethod, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobWipMethod, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobWipMethod()
        {
            TenantId = tenantId
        };
        return OperationResult<JobWipMethod, DomainError>.Ok(entity);
    }
}
