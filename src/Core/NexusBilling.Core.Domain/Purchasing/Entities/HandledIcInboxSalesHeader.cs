using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class HandledIcInboxSalesHeader : Entity
{
    private HandledIcInboxSalesHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string No { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string ShipToName { get; private set; }
    public string ShipToAddress { get; private set; }
    public string ShipToAddress2 { get; private set; }
    public string ShipToCity { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public string CurrencyCode { get; private set; }
    public bool PricesIncludingVat { get; private set; }
    public string ShipToPostCode { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string IcPartnerCode { get; private set; }
    public int IcTransactionNo { get; private set; }
    public short TransactionSource { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }

    public static OperationResult<HandledIcInboxSalesHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<HandledIcInboxSalesHeader, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new HandledIcInboxSalesHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<HandledIcInboxSalesHeader, DomainError>.Ok(entity);
    }
}
