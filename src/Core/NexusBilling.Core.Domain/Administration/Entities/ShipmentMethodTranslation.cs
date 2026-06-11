using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ShipmentMethodTranslation : Entity
{
    private ShipmentMethodTranslation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ShipmentMethod { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<ShipmentMethodTranslation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ShipmentMethodTranslation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ShipmentMethodTranslation()
        {
            TenantId = tenantId
        };
        return OperationResult<ShipmentMethodTranslation, DomainError>.Ok(entity);
    }
}
