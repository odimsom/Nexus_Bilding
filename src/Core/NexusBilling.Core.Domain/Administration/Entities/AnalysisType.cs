using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisType : Entity
{
    private AnalysisType() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public short ValueType { get; private set; }
    public string ItemLedgerEntryTypeFilter { get; private set; }
    public string ValueEntryTypeFilter { get; private set; }

    public static OperationResult<AnalysisType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisType, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisType()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisType, DomainError>.Ok(entity);
    }
}
