using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class ApprovalEntry : Entity
{
    private ApprovalEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int SequenceNo { get; private set; }
    public string ApprovalCode { get; private set; }
    public string SenderId { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string ApproverId { get; private set; }
    public short Status { get; private set; }
    public DateTime? DateTimeSentForApproval { get; private set; }
    public DateTime? LastDateTimeModified { get; private set; }
    public string LastModifiedByUserId { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string CurrencyCode { get; private set; }
    public short ApprovalType { get; private set; }
    public short LimitType { get; private set; }
    public decimal AvailableCreditLimitLcy { get; private set; }
    public string RecordIdToApprove { get; private set; }
    public string DelegationDateFormula { get; private set; }
    public int EntryNo { get; private set; }
    public Guid WorkflowStepInstanceId { get; private set; }

    public static OperationResult<ApprovalEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ApprovalEntry, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ApprovalEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ApprovalEntry, DomainError>.Ok(entity);
    }
}
