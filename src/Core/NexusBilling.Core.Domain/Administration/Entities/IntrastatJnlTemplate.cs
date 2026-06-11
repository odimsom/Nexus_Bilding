using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IntrastatJnlTemplate : Entity
{
    private IntrastatJnlTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int ChecklistReportId { get; private set; }
    public int PageId { get; private set; }

    public static OperationResult<IntrastatJnlTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IntrastatJnlTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IntrastatJnlTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<IntrastatJnlTemplate, DomainError>.Ok(entity);
    }
}
