using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PostValueEntryToGL : Entity
{
    private PostValueEntryToGL() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ValueEntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public DateTime? PostingDate { get; private set; }

    public static OperationResult<PostValueEntryToGL, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostValueEntryToGL, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostValueEntryToGL()
        {
            TenantId = tenantId
        };
        return OperationResult<PostValueEntryToGL, DomainError>.Ok(entity);
    }
}
