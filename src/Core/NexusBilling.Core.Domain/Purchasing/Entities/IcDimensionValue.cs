using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcDimensionValue : Entity
{
    private IcDimensionValue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DimensionCode { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public short DimensionValueType { get; private set; }
    public bool Blocked { get; private set; }
    public string MapToDimensionCode { get; private set; }
    public string MapToDimensionValueCode { get; private set; }
    public int Indentation { get; private set; }

    public static OperationResult<IcDimensionValue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcDimensionValue, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcDimensionValue()
        {
            TenantId = tenantId
        };
        return OperationResult<IcDimensionValue, DomainError>.Ok(entity);
    }
}
