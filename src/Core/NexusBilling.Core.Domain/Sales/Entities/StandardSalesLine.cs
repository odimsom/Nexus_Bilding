using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class StandardSalesLine : Entity
{
    private StandardSalesLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string StandardSalesCode { get; private set; }
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

    public static OperationResult<StandardSalesLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardSalesLine, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardSalesLine()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardSalesLine, DomainError>.Ok(entity);
    }
}
