using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesLineDiscount : Entity
{
    private SalesLineDiscount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string SalesCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public decimal LineDiscount { get; private set; }
    public short SalesType { get; private set; }
    public decimal MinimumQuantity { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public short Type { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string VariantCode { get; private set; }

    public static OperationResult<SalesLineDiscount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesLineDiscount, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesLineDiscount()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesLineDiscount, DomainError>.Ok(entity);
    }
}
