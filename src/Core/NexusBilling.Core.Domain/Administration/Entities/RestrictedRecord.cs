using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class RestrictedRecord : Entity
{
    private RestrictedRecord() { }

    public TenantIdentifier TenantId { get; private set; }
    public long IdNav { get; private set; }
    public string RecordId { get; private set; }
    public string Details { get; private set; }

    public static OperationResult<RestrictedRecord, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RestrictedRecord, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RestrictedRecord()
        {
            TenantId = tenantId
        };
        return OperationResult<RestrictedRecord, DomainError>.Ok(entity);
    }
}
