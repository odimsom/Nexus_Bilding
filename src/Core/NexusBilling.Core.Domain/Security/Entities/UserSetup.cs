using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserSetup : Entity
{
    private UserSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public DateTime? AllowPostingFrom { get; private set; }
    public DateTime? AllowPostingTo { get; private set; }
    public bool RegisterTime { get; private set; }
    public string SalespersPurchCode { get; private set; }
    public string ApproverId { get; private set; }
    public int SalesAmountApprovalLimit { get; private set; }
    public int PurchaseAmountApprovalLimit { get; private set; }
    public bool UnlimitedSalesApproval { get; private set; }
    public bool UnlimitedPurchaseApproval { get; private set; }
    public string Substitute { get; private set; }
    public string EMail { get; private set; }
    public int RequestAmountApprovalLimit { get; private set; }
    public bool UnlimitedRequestApproval { get; private set; }
    public bool ApprovalAdministrator { get; private set; }
    public bool TimeSheetAdmin { get; private set; }
    public DateTime? AllowFaPostingFrom { get; private set; }
    public DateTime? AllowFaPostingTo { get; private set; }
    public string SalesRespCtrFilter { get; private set; }
    public string PurchaseRespCtrFilter { get; private set; }
    public string ServiceRespCtrFilter { get; private set; }

    public static OperationResult<UserSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserSetup, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<UserSetup, DomainError>.Ok(entity);
    }
}
