using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ExtendedTextHeader : Entity
{
    private ExtendedTextHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short TableName { get; private set; }
    public string No { get; private set; }
    public string LanguageCode { get; private set; }
    public int TextNo { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public bool AllLanguageCodes { get; private set; }
    public string Description { get; private set; }
    public bool SalesQuote { get; private set; }
    public bool SalesInvoice { get; private set; }
    public bool SalesOrder { get; private set; }
    public bool SalesCreditMemo { get; private set; }
    public bool PurchaseQuote { get; private set; }
    public bool PurchaseInvoice { get; private set; }
    public bool PurchaseOrder { get; private set; }
    public bool PurchaseCreditMemo { get; private set; }
    public bool Reminder { get; private set; }
    public bool FinanceChargeMemo { get; private set; }
    public bool SalesBlanketOrder { get; private set; }
    public bool PurchaseBlanketOrder { get; private set; }
    public bool PrepmtSalesInvoice { get; private set; }
    public bool PrepmtSalesCreditMemo { get; private set; }
    public bool PrepmtPurchaseInvoice { get; private set; }
    public bool PrepmtPurchaseCreditMemo { get; private set; }
    public bool ServiceOrder { get; private set; }
    public bool ServiceQuote { get; private set; }
    public bool ServiceInvoice { get; private set; }
    public bool ServiceCreditMemo { get; private set; }
    public bool SalesReturnOrder { get; private set; }
    public bool PurchaseReturnOrder { get; private set; }

    public static OperationResult<ExtendedTextHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ExtendedTextHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ExtendedTextHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<ExtendedTextHeader, DomainError>.Ok(entity);
    }
}
