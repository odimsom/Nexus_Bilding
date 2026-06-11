using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLEntryVatEntryLink : Entity
{
    private GLEntryVatEntryLink() { }

    public TenantIdentifier TenantId { get; private set; }
    public int GLEntryNo { get; private set; }
    public int VatEntryNo { get; private set; }

    public static OperationResult<GLEntryVatEntryLink, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLEntryVatEntryLink, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new GLEntryVatEntryLink()
        {
            TenantId = tenantId
        };
        return OperationResult<GLEntryVatEntryLink, DomainError>.Ok(entity);
    }
}
