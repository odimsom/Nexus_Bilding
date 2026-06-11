using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesPrice : Entity
{
    private SalesPrice() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string SalesCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public decimal UnitPrice { get; private set; }
    public bool PriceIncludesVat { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public string VatBusPostingGrPrice { get; private set; }
    public short SalesType { get; private set; }
    public decimal MinimumQuantity { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string VariantCode { get; private set; }
    public bool AllowLineDisc { get; private set; }

    public static OperationResult<SalesPrice, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesPrice, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesPrice()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesPrice, DomainError>.Ok(entity);
    }
}
