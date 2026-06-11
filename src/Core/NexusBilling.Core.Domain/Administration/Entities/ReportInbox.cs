using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ReportInbox : Entity
{
    private ReportInbox() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string UserId { get; private set; }
    public byte[]? ReportOutput { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public Guid JobQueueLogEntryId { get; private set; }
    public short OutputType { get; private set; }
    public string Description { get; private set; }
    public int ReportId { get; private set; }
    public bool Read { get; private set; }

    public static OperationResult<ReportInbox, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReportInbox, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReportInbox()
        {
            TenantId = tenantId
        };
        return OperationResult<ReportInbox, DomainError>.Ok(entity);
    }
}
