using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchasePrice : Entity
{
    private PurchasePrice() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VendorNo { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal MinimumQuantity { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string VariantCode { get; private set; }

    public static OperationResult<PurchasePrice, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchasePrice, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PurchasePrice()
        {
            TenantId = tenantId
        };
        return OperationResult<PurchasePrice, DomainError>.Ok(entity);
    }
}
