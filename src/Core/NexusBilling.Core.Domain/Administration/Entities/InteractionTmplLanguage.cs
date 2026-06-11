using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class InteractionTmplLanguage : Entity
{
    private InteractionTmplLanguage() { }

    public TenantIdentifier TenantId { get; private set; }
    public string InteractionTemplateCode { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }
    public int AttachmentNo { get; private set; }
    public string CustomLayoutCode { get; private set; }

    public static OperationResult<InteractionTmplLanguage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InteractionTmplLanguage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InteractionTmplLanguage()
        {
            TenantId = tenantId
        };
        return OperationResult<InteractionTmplLanguage, DomainError>.Ok(entity);
    }
}
