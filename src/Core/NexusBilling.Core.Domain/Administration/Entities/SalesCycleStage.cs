using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SalesCycleStage : Entity
{
    private SalesCycleStage() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SalesCycleCode { get; private set; }
    public int Stage { get; private set; }
    public string Description { get; private set; }
    public decimal Completed { get; private set; }
    public string ActivityCode { get; private set; }
    public bool QuoteRequired { get; private set; }
    public bool AllowSkip { get; private set; }
    public string DateFormula { get; private set; }

    public static OperationResult<SalesCycleStage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesCycleStage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesCycleStage()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesCycleStage, DomainError>.Ok(entity);
    }
}
