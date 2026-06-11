using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesReceivablesSetup : Entity
{
    private SalesReceivablesSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short DiscountPosting { get; private set; }
    public short CreditWarnings { get; private set; }
    public bool StockoutWarning { get; private set; }
    public bool ShipmentOnInvoice { get; private set; }
    public bool InvoiceRounding { get; private set; }
    public bool ExtDocNoMandatory { get; private set; }
    public string CustomerNos { get; private set; }
    public string QuoteNos { get; private set; }
    public string OrderNos { get; private set; }
    public string InvoiceNos { get; private set; }
    public string PostedInvoiceNos { get; private set; }
    public string CreditMemoNos { get; private set; }
    public string PostedCreditMemoNos { get; private set; }
    public string PostedShipmentNos { get; private set; }
    public string ReminderNos { get; private set; }
    public string IssuedReminderNos { get; private set; }
    public string FinChrgMemoNos { get; private set; }
    public string IssuedFinChrgMNos { get; private set; }
    public string PostedPrepmtInvNos { get; private set; }
    public string PostedPrepmtCrMemoNos { get; private set; }
    public string BlanketOrderNos { get; private set; }
    public bool CalcInvDiscount { get; private set; }
    public short ApplnBetweenCurrencies { get; private set; }
    public bool CopyCommentsBlanketToOrder { get; private set; }
    public bool CopyCommentsOrderToInvoice { get; private set; }
    public bool CopyCommentsOrderToShpt { get; private set; }
    public bool AllowVatDifference { get; private set; }
    public bool CalcInvDiscPerVatId { get; private set; }
    public short LogoPositionOnDocuments { get; private set; }
    public bool CheckPrepmtWhenPosting { get; private set; }
    public short DefaultPostingDate { get; private set; }
    public short DefaultQuantityToShip { get; private set; }
    public bool ArchiveQuotesAndOrders { get; private set; }
    public bool PostWithJobQueue { get; private set; }
    public string JobQueueCategoryCode { get; private set; }
    public int JobQueuePriorityForPost { get; private set; }
    public bool PostPrintWithJobQueue { get; private set; }
    public int JobQPrioForPostPrint { get; private set; }
    public bool NotifyOnSuccess { get; private set; }
    public string VatBusPostingGrPrice { get; private set; }
    public string DirectDebitMandateNos { get; private set; }
    public DateTime? AllowDocumentDeletionBefore { get; private set; }
    public bool DefaultItemQuantity { get; private set; }
    public bool CreateItemFromDescription { get; private set; }
    public string PostedReturnReceiptNos { get; private set; }
    public bool CopyCmtsRetOrdToRetRcpt { get; private set; }
    public bool CopyCmtsRetOrdToCrMemo { get; private set; }
    public string ReturnOrderNos { get; private set; }
    public bool ReturnReceiptOnCreditMemo { get; private set; }
    public bool ExactCostReversingMandatory { get; private set; }
    public string CustomerGroupDimensionCode { get; private set; }
    public string SalespersonDimensionCode { get; private set; }
    public string FreightGLAccNo { get; private set; }

    public static OperationResult<SalesReceivablesSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesReceivablesSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesReceivablesSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesReceivablesSetup, DomainError>.Ok(entity);
    }
}
