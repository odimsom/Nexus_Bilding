using System;

namespace NexusBilling.Core.Application.Security.DTOs;

public record UserSetupDto(
    string UserId,
    DateTime? AllowPostingFrom,
    DateTime? AllowPostingTo,
    bool RegisterTime,
    string SalespersPurchCode,
    string ApproverId,
    int SalesAmountApprovalLimit,
    int PurchaseAmountApprovalLimit,
    bool UnlimitedSalesApproval,
    bool UnlimitedPurchaseApproval,
    string EMail,
    bool ApprovalAdministrator,
    bool TimeSheetAdmin
);
