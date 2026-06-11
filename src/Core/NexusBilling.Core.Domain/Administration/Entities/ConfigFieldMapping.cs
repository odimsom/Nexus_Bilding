using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigFieldMapping : Entity
{
    private ConfigFieldMapping() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }
    public string FieldName { get; private set; }
    public string OldValue { get; private set; }
    public string NewValue { get; private set; }

    public static OperationResult<ConfigFieldMapping, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigFieldMapping, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigFieldMapping()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigFieldMapping, DomainError>.Ok(entity);
    }
}
