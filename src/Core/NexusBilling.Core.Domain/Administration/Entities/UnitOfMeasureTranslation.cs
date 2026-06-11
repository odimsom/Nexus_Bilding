using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class UnitOfMeasureTranslation : Entity
{
    private UnitOfMeasureTranslation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<UnitOfMeasureTranslation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UnitOfMeasureTranslation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UnitOfMeasureTranslation()
        {
            TenantId = tenantId
        };
        return OperationResult<UnitOfMeasureTranslation, DomainError>.Ok(entity);
    }
}
