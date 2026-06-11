using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ToDoInteractionLanguage : Entity
{
    private ToDoInteractionLanguage() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ToDoNo { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }
    public int AttachmentNo { get; private set; }

    public static OperationResult<ToDoInteractionLanguage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ToDoInteractionLanguage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ToDoInteractionLanguage()
        {
            TenantId = tenantId
        };
        return OperationResult<ToDoInteractionLanguage, DomainError>.Ok(entity);
    }
}
