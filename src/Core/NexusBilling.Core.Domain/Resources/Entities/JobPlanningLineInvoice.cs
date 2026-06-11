using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobPlanningLineInvoice : Entity
{
    private JobPlanningLineInvoice() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public int JobPlanningLineNo { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public decimal QuantityTransferred { get; private set; }
    public DateTime? TransferredDate { get; private set; }
    public DateTime? InvoicedDate { get; private set; }
    public decimal InvoicedAmountLcy { get; private set; }
    public decimal InvoicedCostAmountLcy { get; private set; }
    public int JobLedgerEntryNo { get; private set; }

    public static OperationResult<JobPlanningLineInvoice, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobPlanningLineInvoice, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobPlanningLineInvoice()
        {
            TenantId = tenantId
        };
        return OperationResult<JobPlanningLineInvoice, DomainError>.Ok(entity);
    }
}
