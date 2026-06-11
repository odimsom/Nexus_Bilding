using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackage : Entity
{
    private ConfigPackage() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string PackageName { get; private set; }
    public int LanguageId { get; private set; }
    public string ProductVersion { get; private set; }
    public bool ExcludeConfigTables { get; private set; }
    public int ProcessingOrder { get; private set; }

    public static OperationResult<ConfigPackage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackage()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackage, DomainError>.Ok(entity);
    }
}
