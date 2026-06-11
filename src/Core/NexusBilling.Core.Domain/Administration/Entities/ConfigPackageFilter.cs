using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageFilter : Entity
{
    private ConfigPackageFilter() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int ProcessingRuleNo { get; private set; }
    public int FieldId { get; private set; }
    public string FieldFilter { get; private set; }

    public static OperationResult<ConfigPackageFilter, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageFilter, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageFilter()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageFilter, DomainError>.Ok(entity);
    }
}
