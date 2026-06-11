using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ReportLayoutSelection : Entity
{
    private ReportLayoutSelection() { }

    public TenantIdentifier TenantId { get; private set; }
    public int ReportId { get; private set; }
    public string ReportName { get; private set; }
    public string CompanyName { get; private set; }
    public short Type { get; private set; }
    public string CustomReportLayoutCode { get; private set; }

    public static OperationResult<ReportLayoutSelection, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReportLayoutSelection, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReportLayoutSelection()
        {
            TenantId = tenantId
        };
        return OperationResult<ReportLayoutSelection, DomainError>.Ok(entity);
    }
}
