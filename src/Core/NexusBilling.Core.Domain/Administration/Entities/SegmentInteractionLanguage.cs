using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SegmentInteractionLanguage : Entity
{
    private SegmentInteractionLanguage() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SegmentNo { get; private set; }
    public int SegmentLineNo { get; private set; }
    public string LanguageCode { get; private set; }
    public string Description { get; private set; }
    public int AttachmentNo { get; private set; }
    public string Subject { get; private set; }

    public static OperationResult<SegmentInteractionLanguage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SegmentInteractionLanguage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SegmentInteractionLanguage()
        {
            TenantId = tenantId
        };
        return OperationResult<SegmentInteractionLanguage, DomainError>.Ok(entity);
    }
}
