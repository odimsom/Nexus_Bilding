using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchasesPayablesSetup : Entity
{
    private PurchasesPayablesSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short DiscountPosting { get; private set; }
    public bool ReceiptOnInvoice { get; private set; }
    public bool InvoiceRounding { get; private set; }
    public bool ExtDocNoMandatory { get; private set; }
    public string VendorNos { get; private set; }
    public string QuoteNos { get; private set; }
    public string OrderNos { get; private set; }
    public string InvoiceNos { get; private set; }
    public string PostedInvoiceNos { get; private set; }
    public string CreditMemoNos { get; private set; }
    public string PostedCreditMemoNos { get; private set; }
    public string PostedReceiptNos { get; private set; }
    public string BlanketOrderNos { get; private set; }
    public bool CalcInvDiscount { get; private set; }
    public short ApplnBetweenCurrencies { get; private set; }
    public bool CopyCommentsBlanketToOrder { get; private set; }
    public bool CopyCommentsOrderToInvoice { get; private set; }
    public bool CopyCommentsOrderToReceipt { get; private set; }
    public bool AllowVatDifference { get; private set; }
    public bool CalcInvDiscPerVatId { get; private set; }
    public string PostedPrepmtInvNos { get; private set; }
    public string PostedPrepmtCrMemoNos { get; private set; }
    public bool CheckPrepmtWhenPosting { get; private set; }
    public short DefaultPostingDate { get; private set; }
    public short DefaultQtyToReceive { get; private set; }
    public bool ArchiveQuotesAndOrders { get; private set; }
    public bool PostWithJobQueue { get; private set; }
    public string JobQueueCategoryCode { get; private set; }
    public int JobQueuePriorityForPost { get; private set; }
    public bool PostPrintWithJobQueue { get; private set; }
    public int JobQPrioForPostPrint { get; private set; }
    public bool NotifyOnSuccess { get; private set; }
    public DateTime? AllowDocumentDeletionBefore { get; private set; }
    public string DebitAccForNonItemLines { get; private set; }
    public string CreditAccForNonItemLines { get; private set; }
    public string PostedReturnShptNos { get; private set; }
    public bool CopyCmtsRetOrdToRetShpt { get; private set; }
    public bool CopyCmtsRetOrdToCrMemo { get; private set; }
    public string ReturnOrderNos { get; private set; }
    public bool ReturnShipmentOnCreditMemo { get; private set; }
    public bool ExactCostReversingMandatory { get; private set; }

    public static OperationResult<PurchasesPayablesSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchasesPayablesSetup, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchasesPayablesSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchasesPayablesSetup, DomainError>.Ok(entity);
    }
}
