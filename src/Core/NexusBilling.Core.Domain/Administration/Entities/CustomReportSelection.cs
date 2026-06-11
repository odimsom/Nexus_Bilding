using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CustomReportSelection : Entity
{
    private CustomReportSelection() { }

    public TenantIdentifier TenantId { get; private set; }
    public int SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public short Usage { get; private set; }
    public int Sequence { get; private set; }
    public int ReportId { get; private set; }
    public string CustomReportLayoutCode { get; private set; }
    public string SendToEmail { get; private set; }
    public bool UseForEmailAttachment { get; private set; }
    public bool UseForEmailBody { get; private set; }
    public string EmailBodyLayoutCode { get; private set; }

    public static OperationResult<CustomReportSelection, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustomReportSelection, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustomReportSelection()
        {
            TenantId = tenantId
        };
        return OperationResult<CustomReportSelection, DomainError>.Ok(entity);
    }
}
