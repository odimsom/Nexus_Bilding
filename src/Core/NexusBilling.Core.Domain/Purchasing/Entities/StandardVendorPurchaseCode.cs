using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class StandardVendorPurchaseCode : Entity
{
    private StandardVendorPurchaseCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string VendorNo { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<StandardVendorPurchaseCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardVendorPurchaseCode, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardVendorPurchaseCode()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardVendorPurchaseCode, DomainError>.Ok(entity);
    }
}
