using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobTaskDimension : Entity
{
    private JobTaskDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueCode { get; private set; }
    public short MultipleSelectionAction { get; private set; }

    public static OperationResult<JobTaskDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobTaskDimension, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobTaskDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<JobTaskDimension, DomainError>.Ok(entity);
    }
}
