using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatStatementName : Entity
{
    private VatStatementName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string StatementTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<VatStatementName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatStatementName, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatStatementName()
        {
            TenantId = tenantId
        };
        return OperationResult<VatStatementName, DomainError>.Ok(entity);
    }
}
