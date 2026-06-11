using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcInboxSalesLine : Entity
{
    private IcInboxSalesLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public string Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal AmountIncludingVat { get; private set; }
    public string JobNo { get; private set; }
    public bool DropShipment { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal LineAmount { get; private set; }
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; }
    public string IcPartnerCode { get; private set; }
    public int IcTransactionNo { get; private set; }
    public short TransactionSource { get; private set; }
    public short ItemRef { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }

    public static OperationResult<IcInboxSalesLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcInboxSalesLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcInboxSalesLine()
        {
            TenantId = tenantId
        };
        return OperationResult<IcInboxSalesLine, DomainError>.Ok(entity);
    }
}
