using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TariffNumber : Entity
{
    private TariffNumber() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public bool SupplementaryUnits { get; private set; }

    public static OperationResult<TariffNumber, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TariffNumber, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TariffNumber()
        {
            TenantId = tenantId
        };
        return OperationResult<TariffNumber, DomainError>.Ok(entity);
    }
}
