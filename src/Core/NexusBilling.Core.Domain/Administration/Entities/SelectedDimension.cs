using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SelectedDimension : Entity
{
    private SelectedDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public int ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public string DimensionCode { get; private set; }
    public string NewDimensionValueCode { get; private set; }
    public string DimensionValueFilter { get; private set; }
    public short Level { get; private set; }
    public string AnalysisViewCode { get; private set; }

    public static OperationResult<SelectedDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SelectedDimension, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SelectedDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<SelectedDimension, DomainError>.Ok(entity);
    }
}
