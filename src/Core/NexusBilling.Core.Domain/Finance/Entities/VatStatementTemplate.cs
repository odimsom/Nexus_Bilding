using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatStatementTemplate : Entity
{
    private VatStatementTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int PageId { get; private set; }
    public int VatStatementReportId { get; private set; }

    public static OperationResult<VatStatementTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatStatementTemplate, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatStatementTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<VatStatementTemplate, DomainError>.Ok(entity);
    }
}
