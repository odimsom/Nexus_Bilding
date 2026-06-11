using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IncomingDocumentAttachment : Entity
{
    private IncomingDocumentAttachment() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IncomingDocumentEntryNo { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public string CreatedByUserName { get; private set; }
    public string Name { get; private set; }
    public short Type { get; private set; }
    public string FileExtension { get; private set; }
    public byte[]? Content { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public bool Default { get; private set; }
    public bool UseForOcr { get; private set; }
    public string ExternalDocumentReference { get; private set; }
    public string OcrServiceDocumentReference { get; private set; }
    public bool GeneratedFromOcr { get; private set; }
    public bool MainAttachment { get; private set; }

    public static OperationResult<IncomingDocumentAttachment, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IncomingDocumentAttachment, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IncomingDocumentAttachment()
        {
            TenantId = tenantId
        };
        return OperationResult<IncomingDocumentAttachment, DomainError>.Ok(entity);
    }
}
