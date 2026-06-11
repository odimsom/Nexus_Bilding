using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AssistedCompanySetupStatus : Entity
{
    private AssistedCompanySetupStatus() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CompanyName { get; private set; }
    public bool Enabled { get; private set; }
    public bool PackageImported { get; private set; }
    public bool ImportFailed { get; private set; }
    public int CompanySetupSessionId { get; private set; }

    public static OperationResult<AssistedCompanySetupStatus, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssistedCompanySetupStatus, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssistedCompanySetupStatus()
        {
            TenantId = tenantId
        };
        return OperationResult<AssistedCompanySetupStatus, DomainError>.Ok(entity);
    }
}
