using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ChangeLogSetupTable : Entity
{
    private ChangeLogSetupTable() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableNo { get; private set; }
    public short LogInsertion { get; private set; }
    public short LogModification { get; private set; }
    public short LogDeletion { get; private set; }

    public static OperationResult<ChangeLogSetupTable, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ChangeLogSetupTable, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ChangeLogSetupTable()
        {
            TenantId = tenantId
        };
        return OperationResult<ChangeLogSetupTable, DomainError>.Ok(entity);
    }
}
