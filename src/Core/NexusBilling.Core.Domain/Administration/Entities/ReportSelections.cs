using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ReportSelections : Entity
{
    private ReportSelections() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Usage { get; private set; }
    public string Sequence { get; private set; }
    public int ReportId { get; private set; }
    public string CustomReportLayoutCode { get; private set; }
    public bool UseForEmailAttachment { get; private set; }
    public bool UseForEmailBody { get; private set; }
    public string EmailBodyLayoutCode { get; private set; }

    public static OperationResult<ReportSelections, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReportSelections, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReportSelections()
        {
            TenantId = tenantId
        };
        return OperationResult<ReportSelections, DomainError>.Ok(entity);
    }
}
