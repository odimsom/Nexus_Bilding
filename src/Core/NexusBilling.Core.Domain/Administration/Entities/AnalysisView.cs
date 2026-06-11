using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisView : Entity
{
    private AnalysisView() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public short AccountSource { get; private set; }
    public int LastEntryNo { get; private set; }
    public int LastBudgetEntryNo { get; private set; }
    public DateTime? LastDateUpdated { get; private set; }
    public bool UpdateOnPosting { get; private set; }
    public bool Blocked { get; private set; }
    public string AccountFilter { get; private set; }
    public string BusinessUnitFilter { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public short DateCompression { get; private set; }
    public string Dimension1Code { get; private set; }
    public string Dimension2Code { get; private set; }
    public string Dimension3Code { get; private set; }
    public string Dimension4Code { get; private set; }
    public bool IncludeBudgets { get; private set; }
    public bool RefreshWhenUnblocked { get; private set; }

    public static OperationResult<AnalysisView, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisView, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisView()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisView, DomainError>.Ok(entity);
    }
}
