using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NoSeriesLine : Entity
{
    private NoSeriesLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SeriesCode { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string StartingNo { get; private set; }
    public string EndingNo { get; private set; }
    public string WarningNo { get; private set; }
    public int IncrementByNo { get; private set; }
    public string LastNoUsed { get; private set; }
    public bool Open { get; private set; }
    public DateTime? LastDateUsed { get; private set; }

    public static OperationResult<NoSeriesLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NoSeriesLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NoSeriesLine()
        {
            TenantId = tenantId
        };
        return OperationResult<NoSeriesLine, DomainError>.Ok(entity);
    }
}
