using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemTranslation : Entity
{
    private ItemTranslation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string VariantCode { get; private set; }

    public static OperationResult<ItemTranslation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemTranslation, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemTranslation()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemTranslation, DomainError>.Ok(entity);
    }
}
