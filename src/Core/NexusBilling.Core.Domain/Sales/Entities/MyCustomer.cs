using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class MyCustomer : Entity
{
    private MyCustomer() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string CustomerNo { get; private set; }
    public string Name { get; private set; }
    public string PhoneNo { get; private set; }

    public static OperationResult<MyCustomer, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MyCustomer, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MyCustomer()
        {
            TenantId = tenantId
        };
        return OperationResult<MyCustomer, DomainError>.Ok(entity);
    }
}
