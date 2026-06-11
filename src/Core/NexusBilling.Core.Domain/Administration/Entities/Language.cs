using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Language : Entity
{
    private Language() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int WindowsLanguageId { get; private set; }

    public static OperationResult<Language, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Language, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Language()
        {
            TenantId = tenantId
        };
        return OperationResult<Language, DomainError>.Ok(entity);
    }
}
