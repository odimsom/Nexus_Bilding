using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class MyVendor : Entity
{
    private MyVendor() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string VendorNo { get; private set; }
    public string Name { get; private set; }
    public string PhoneNo { get; private set; }

    public static OperationResult<MyVendor, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MyVendor, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MyVendor()
        {
            TenantId = tenantId
        };
        return OperationResult<MyVendor, DomainError>.Ok(entity);
    }
}
