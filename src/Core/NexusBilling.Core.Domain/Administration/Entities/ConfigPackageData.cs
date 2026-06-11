using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageData : Entity
{
    private ConfigPackageData() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int No { get; private set; }
    public int FieldId { get; private set; }
    public string Value { get; private set; }
    public bool Invalid { get; private set; }
    public byte[]? BlobValue { get; private set; }

    public static OperationResult<ConfigPackageData, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageData, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageData()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageData, DomainError>.Ok(entity);
    }
}
