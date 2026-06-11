using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceItemGroup : Entity
{
    private ServiceItemGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool CreateServiceItem { get; private set; }
    public decimal DefaultContractDiscount { get; private set; }
    public string DefaultServPriceGroupCode { get; private set; }
    public decimal DefaultResponseTimeHours { get; private set; }

    public static OperationResult<ServiceItemGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceItemGroup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceItemGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceItemGroup, DomainError>.Ok(entity);
    }
}
