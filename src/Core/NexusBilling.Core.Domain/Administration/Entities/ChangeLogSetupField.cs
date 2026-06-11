using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ChangeLogSetupField : Entity
{
    private ChangeLogSetupField() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableNo { get; private set; }
    public int FieldNo { get; private set; }
    public bool LogInsertion { get; private set; }
    public bool LogModification { get; private set; }
    public bool LogDeletion { get; private set; }

    public static OperationResult<ChangeLogSetupField, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ChangeLogSetupField, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ChangeLogSetupField()
        {
            TenantId = tenantId
        };
        return OperationResult<ChangeLogSetupField, DomainError>.Ok(entity);
    }
}
