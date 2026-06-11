using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class StandardItemJournal : Entity
{
    private StandardItemJournal() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<StandardItemJournal, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardItemJournal, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardItemJournal()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardItemJournal, DomainError>.Ok(entity);
    }
}
