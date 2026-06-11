using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WhseWorksheetTemplate : Entity
{
    private WhseWorksheetTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public short Type { get; private set; }
    public int PageId { get; private set; }

    public static OperationResult<WhseWorksheetTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WhseWorksheetTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WhseWorksheetTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<WhseWorksheetTemplate, DomainError>.Ok(entity);
    }
}
