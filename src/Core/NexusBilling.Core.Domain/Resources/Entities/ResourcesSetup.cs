using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResourcesSetup : Entity
{
    private ResourcesSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string ResourceNos { get; private set; }
    public string TimeSheetNos { get; private set; }
    public short TimeSheetFirstWeekday { get; private set; }
    public short TimeSheetByJobApproval { get; private set; }

    public static OperationResult<ResourcesSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourcesSetup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourcesSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourcesSetup, DomainError>.Ok(entity);
    }
}
