using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OfficeAddIn : Entity
{
    private OfficeAddIn() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid ApplicationId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Version { get; private set; }
    public int ManifestCodeunit { get; private set; }
    public DateTime? DeploymentDate { get; private set; }
    public byte[]? DefaultManifest { get; private set; }
    public byte[]? Manifest { get; private set; }
    public bool Breaking { get; private set; }

    public static OperationResult<OfficeAddIn, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OfficeAddIn, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OfficeAddIn()
        {
            TenantId = tenantId
        };
        return OperationResult<OfficeAddIn, DomainError>.Ok(entity);
    }
}
