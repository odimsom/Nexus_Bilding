using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionTranslation : Entity
{
    private DimensionTranslation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public int LanguageId { get; private set; }
    public string Name { get; private set; }
    public string CodeCaption { get; private set; }
    public string FilterCaption { get; private set; }

    public static OperationResult<DimensionTranslation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionTranslation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionTranslation()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionTranslation, DomainError>.Ok(entity);
    }
}
