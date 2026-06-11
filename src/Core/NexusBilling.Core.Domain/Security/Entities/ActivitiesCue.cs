using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class ActivitiesCue : Entity
{
    private ActivitiesCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public decimal SalesThisMonth { get; private set; }
    public decimal Top10CustomerSalesYtd { get; private set; }
    public decimal OverduePurchInvoiceAmount { get; private set; }
    public decimal OverdueSalesInvoiceAmount { get; private set; }
    public decimal AverageCollectionDays { get; private set; }

    public static OperationResult<ActivitiesCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ActivitiesCue, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ActivitiesCue()
        {
            TenantId = tenantId
        };
        return OperationResult<ActivitiesCue, DomainError>.Ok(entity);
    }
}
