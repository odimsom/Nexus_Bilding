using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceEmailQueue : Entity
{
    private ServiceEmailQueue() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ToAddress { get; private set; }
    public string CopyToAddress { get; private set; }
    public string SubjectLine { get; private set; }
    public string BodyLine { get; private set; }
    public string AttachmentFilename { get; private set; }
    public DateTime? SendingDate { get; private set; }
    public string SendingTime { get; private set; }
    public short Status { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }

    public static OperationResult<ServiceEmailQueue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceEmailQueue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceEmailQueue()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceEmailQueue, DomainError>.Ok(entity);
    }
}
