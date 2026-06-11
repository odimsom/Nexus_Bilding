using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigTableProcessingRule : Entity
{
    private ConfigTableProcessingRule() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int RuleNo { get; private set; }
    public short Action { get; private set; }
    public int CustomProcessingCodeunitId { get; private set; }

    public static OperationResult<ConfigTableProcessingRule, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigTableProcessingRule, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigTableProcessingRule()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigTableProcessingRule, DomainError>.Ok(entity);
    }
}
