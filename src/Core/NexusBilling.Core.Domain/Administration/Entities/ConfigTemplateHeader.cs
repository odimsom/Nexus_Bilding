using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigTemplateHeader : Entity
{
    private ConfigTemplateHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public int TableId { get; private set; }
    public bool Enabled { get; private set; }
    public string InstanceNoSeries { get; private set; }

    public static OperationResult<ConfigTemplateHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigTemplateHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigTemplateHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigTemplateHeader, DomainError>.Ok(entity);
    }
}
