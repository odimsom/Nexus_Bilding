using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CashFlowAccount : Entity
{
    private CashFlowAccount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public short AccountType { get; private set; }
    public bool Blocked { get; private set; }
    public bool NewPage { get; private set; }
    public int NoOfBlankLines { get; private set; }
    public int Indentation { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string Totaling { get; private set; }
    public short SourceType { get; private set; }
    public short GLIntegration { get; private set; }
    public string GLAccountFilter { get; private set; }

    public static OperationResult<CashFlowAccount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CashFlowAccount, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CashFlowAccount()
        {
            TenantId = tenantId
        };
        return OperationResult<CashFlowAccount, DomainError>.Ok(entity);
    }
}
