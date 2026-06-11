using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageRecord : Entity
{
    private ConfigPackageRecord() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int No { get; private set; }
    public bool Invalid { get; private set; }
    public int ParentRecordNo { get; private set; }

    public static OperationResult<ConfigPackageRecord, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageRecord, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageRecord()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageRecord, DomainError>.Ok(entity);
    }
}
