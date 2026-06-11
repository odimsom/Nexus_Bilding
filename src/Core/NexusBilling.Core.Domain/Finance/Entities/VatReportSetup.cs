using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatReportSetup : Entity
{
    private VatReportSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string NoSeries { get; private set; }
    public bool ModifySubmittedReports { get; private set; }

    public static OperationResult<VatReportSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatReportSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatReportSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<VatReportSetup, DomainError>.Ok(entity);
    }
}
