using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CashFlowReportSelection : Entity
{
    private CashFlowReportSelection() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Sequence { get; private set; }
    public int ReportId { get; private set; }

    public static OperationResult<CashFlowReportSelection, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CashFlowReportSelection, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CashFlowReportSelection()
        {
            TenantId = tenantId
        };
        return OperationResult<CashFlowReportSelection, DomainError>.Ok(entity);
    }
}
