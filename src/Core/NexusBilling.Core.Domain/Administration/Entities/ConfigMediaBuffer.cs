using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigMediaBuffer : Entity
{
    private ConfigMediaBuffer() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public Guid MediaSetId { get; private set; }
    public Guid MediaId { get; private set; }
    public int No { get; private set; }
    public byte[]? MediaBlob { get; private set; }
    public Guid MediaSet { get; private set; }
    public Guid Media { get; private set; }

    public static OperationResult<ConfigMediaBuffer, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigMediaBuffer, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigMediaBuffer()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigMediaBuffer, DomainError>.Ok(entity);
    }
}
