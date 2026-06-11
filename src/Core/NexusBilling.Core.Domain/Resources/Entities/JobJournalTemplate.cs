using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobJournalTemplate : Entity
{
    private JobJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int TestReportId { get; private set; }
    public int PageId { get; private set; }
    public int PostingReportId { get; private set; }
    public bool ForcePostingReport { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public bool Recurring { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }

    public static OperationResult<JobJournalTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobJournalTemplate, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobJournalTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<JobJournalTemplate, DomainError>.Ok(entity);
    }
}
