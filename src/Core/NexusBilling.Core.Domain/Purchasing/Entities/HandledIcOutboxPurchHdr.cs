using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class HandledIcOutboxPurchHdr : Entity
{
    private HandledIcOutboxPurchHdr() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string BuyFromVendorNo { get; private set; }
    public string No { get; private set; }
    public string PayToVendorNo { get; private set; }
    public string YourReference { get; private set; }
    public string ShipToName { get; private set; }
    public string ShipToAddress { get; private set; }
    public string ShipToAddress2 { get; private set; }
    public string ShipToCity { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public string CurrencyCode { get; private set; }
    public bool PricesIncludingVat { get; private set; }
    public string VendorInvoiceNo { get; private set; }
    public string VendorCrMemoNo { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string ShipToPostCode { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string IcPartnerCode { get; private set; }
    public int IcTransactionNo { get; private set; }
    public short TransactionSource { get; private set; }
    public DateTime? RequestedReceiptDate { get; private set; }
    public DateTime? PromisedReceiptDate { get; private set; }

    public static OperationResult<HandledIcOutboxPurchHdr, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<HandledIcOutboxPurchHdr, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new HandledIcOutboxPurchHdr()
        {
            TenantId = tenantId
        };
        return OperationResult<HandledIcOutboxPurchHdr, DomainError>.Ok(entity);
    }
}
