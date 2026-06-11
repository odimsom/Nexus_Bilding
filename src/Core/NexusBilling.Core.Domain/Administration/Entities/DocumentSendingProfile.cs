using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DocumentSendingProfile : Entity
{
    private DocumentSendingProfile() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short Printer { get; private set; }
    public short EMail { get; private set; }
    public short EMailAttachment { get; private set; }
    public string EMailFormat { get; private set; }
    public short Disk { get; private set; }
    public string DiskFormat { get; private set; }
    public short ElectronicDocument { get; private set; }
    public string ElectronicFormat { get; private set; }
    public bool Default { get; private set; }
    public short SendTo { get; private set; }
    public short Usage { get; private set; }
    public bool OneRelatedPartySelected { get; private set; }

    public static OperationResult<DocumentSendingProfile, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DocumentSendingProfile, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DocumentSendingProfile()
        {
            TenantId = tenantId
        };
        return OperationResult<DocumentSendingProfile, DomainError>.Ok(entity);
    }
}
