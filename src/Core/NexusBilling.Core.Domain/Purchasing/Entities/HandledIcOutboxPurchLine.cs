using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class HandledIcOutboxPurchLine : Entity
{
    private HandledIcOutboxPurchLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public string Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal AmountIncludingVat { get; private set; }
    public string JobNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public bool DropShipment { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal LineAmount { get; private set; }
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; }
    public string IcPartnerCode { get; private set; }
    public int IcTransactionNo { get; private set; }
    public short TransactionSource { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public DateTime? RequestedReceiptDate { get; private set; }
    public DateTime? PromisedReceiptDate { get; private set; }

    public static OperationResult<HandledIcOutboxPurchLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<HandledIcOutboxPurchLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new HandledIcOutboxPurchLine()
        {
            TenantId = tenantId
        };
        return OperationResult<HandledIcOutboxPurchLine, DomainError>.Ok(entity);
    }
}
