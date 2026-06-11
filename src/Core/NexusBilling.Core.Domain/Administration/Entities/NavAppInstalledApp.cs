using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NavAppInstalledApp : Entity
{
    private NavAppInstalledApp() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid AppId { get; private set; }
    public Guid PackageId { get; private set; }
    public string Name { get; private set; }
    public string Publisher { get; private set; }
    public int VersionMajor { get; private set; }
    public int VersionMinor { get; private set; }
    public int VersionBuild { get; private set; }
    public int VersionRevision { get; private set; }
    public int CompatibilityMajor { get; private set; }
    public int CompatibilityMinor { get; private set; }
    public int CompatibilityBuild { get; private set; }
    public int CompatibilityRevision { get; private set; }

    public static OperationResult<NavAppInstalledApp, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NavAppInstalledApp, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NavAppInstalledApp()
        {
            TenantId = tenantId
        };
        return OperationResult<NavAppInstalledApp, DomainError>.Ok(entity);
    }
}
