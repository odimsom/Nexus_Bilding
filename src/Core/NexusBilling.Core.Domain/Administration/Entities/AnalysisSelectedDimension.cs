using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisSelectedDimension : Entity
{
    private AnalysisSelectedDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public int ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public string DimensionCode { get; private set; }
    public string NewDimensionValueCode { get; private set; }
    public string DimensionValueFilter { get; private set; }
    public short Level { get; private set; }
    public string AnalysisViewCode { get; private set; }
    public short AnalysisArea { get; private set; }

    public static OperationResult<AnalysisSelectedDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisSelectedDimension, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisSelectedDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisSelectedDimension, DomainError>.Ok(entity);
    }
}
