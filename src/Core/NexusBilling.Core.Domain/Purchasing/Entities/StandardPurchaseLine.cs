using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class StandardPurchaseLine : Entity
{
    private StandardPurchaseLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string StandardPurchaseCode { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AmountExclVat { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string VariantCode { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<StandardPurchaseLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardPurchaseLine, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardPurchaseLine()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardPurchaseLine, DomainError>.Ok(entity);
    }
}
