using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class PageDocumentation : Entity
{
    private PageDocumentation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int PageId { get; private set; }
    public string RelativePath { get; private set; }

    public static OperationResult<PageDocumentation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PageDocumentation, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PageDocumentation()
        {
            TenantId = tenantId
        };
        return OperationResult<PageDocumentation, DomainError>.Ok(entity);
    }
}
