using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionValue : Entity
{
    private DimensionValue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DimensionCode { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public short DimensionValueType { get; private set; }
    public string Totaling { get; private set; }
    public bool Blocked { get; private set; }
    public string ConsolidationCode { get; private set; }
    public int Indentation { get; private set; }
    public int GlobalDimensionNo { get; private set; }
    public string MapToIcDimensionCode { get; private set; }
    public string MapToIcDimensionValueCode { get; private set; }
    public int DimensionValueId { get; private set; }

    public static OperationResult<DimensionValue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionValue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionValue()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionValue, DomainError>.Ok(entity);
    }
}
