using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class PayrollSetup : Entity
{
    private PayrollSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public int PrimaryKey { get; private set; }
    public string GeneralJournalTemplateName { get; private set; }
    public string GeneralJournalBatchName { get; private set; }
    public string UserName { get; private set; }

    public static OperationResult<PayrollSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PayrollSetup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PayrollSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<PayrollSetup, DomainError>.Ok(entity);
    }
}
