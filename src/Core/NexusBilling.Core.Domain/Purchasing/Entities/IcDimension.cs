using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcDimension : Entity
{
    private IcDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public bool Blocked { get; private set; }
    public string MapToDimensionCode { get; private set; }

    public static OperationResult<IcDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcDimension, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<IcDimension, DomainError>.Ok(entity);
    }
}
